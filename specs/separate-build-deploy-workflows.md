## Problem Statement

The iOS and Android deploy workflows currently rebuild the entire app from scratch when a deploy is triggered. This means the binary that gets published to TestFlight or Google Play is **not the same binary** that was built and verified during CI. The rebuild also duplicates significant build time (cert installation, .NET MAUI workload install, dotnet publish) and keeps all signing secrets available during every build, not just deploys.

Additionally, the Android build workflow produces an unsigned AAB — signing was only applied to a sideload APK via bundletool, not to the AAB that gets uploaded to Google Play.

## Solution

Restructure the four workflow files so that:
- Build workflows (`android-build.yml`, `macos-build.yml`) run on push/PR, produce a fully signed artifact, and upload it.
- Deploy workflows (`deploy-android.yml`, `deploy-ios.yml`) are triggered manually via `workflow_dispatch`, locate the latest successful build artifact on a specified branch (default: `main`), download it without rebuilding, and ship it.

This guarantees the deployed binary is identical to what CI verified, eliminates duplicate build time, and gives explicit human control over when a release goes out.

## User Stories

1. As a developer, I want the deploy workflow to ship the exact binary that CI built and verified, so that I have confidence the released app matches what was tested.
2. As a developer, I want to trigger a deploy manually via `workflow_dispatch`, so that I control exactly when a release goes out.
3. As a developer, I want to specify a branch when manually triggering a deploy (defaulting to `main`), so that I can deploy a hotfix branch without changing the workflow file.
4. As a developer, I want the Android build workflow to produce a signed AAB, so that the artifact is immediately ready for Google Play upload without further processing.
5. As a developer, I want the deploy workflow to find the latest successful build on the target branch automatically, so that I don't have to look up run IDs manually.
6. As a developer, I want the iOS and Android deploy workflows to be triggered independently, so that I can ship one platform without touching the other.
7. As a developer, I want to remove the git tag trigger from deploy workflows, so that deploys are always explicit and intentional rather than automatic.
8. As a developer, I want the bundletool APK conversion removed from the Android build workflow, so that the build is leaner and the artifact is clearly scoped to what gets deployed.
9. As a developer, I want Android signing to happen in the build workflow using MAUI's MSBuild signing properties, so that the AAB artifact is production-signed before upload to any artifact store.
10. As a developer, I want the iOS artifact name to be consistent between the build and deploy workflows, so that cross-workflow artifact downloads work without manual name matching.

## Implementation Decisions

- **android-build.yml**: Remove the standalone `dotnet build` step (redundant — `dotnet publish` already builds). Add keystore decode and pass signing parameters to `dotnet publish` via `/p:AndroidKeyStore=true`, `/p:AndroidSigningKeyStore`, `/p:AndroidSigningKeyAlias`, `/p:AndroidSigningKeyPass`, `/p:AndroidSigningStorePass`. Remove the bundletool download, keystore decode (old), and APK conversion steps. Narrow the artifact upload to the AAB file only. Artifact name stays `android-app`.

- **deploy-android.yml**: Remove the `push: tags` trigger; keep only `workflow_dispatch`. Add a `branch` input (default: `main`). Remove the `build` job entirely. In the deploy job: call the GitHub API to find the latest successful run of `android-build.yml` on the specified branch, then use `actions/download-artifact` with that run ID to download the `android-app` artifact. Add `actions: read` permission to the deploy job.

- **macos-build.yml (iOS build)**: Rename the uploaded artifact from `artifacts-ios` to `ios-app` to match what the deploy workflow expects.

- **deploy-ios.yml**: Remove the `push: tags` trigger; keep only `workflow_dispatch`. Add a `branch` input (default: `main`). Remove the `build` job entirely (certificate installation, .NET MAUI workload install, dotnet publish). In the deploy job: call the GitHub API to find the latest successful run of `macos-build.yml` on the specified branch, then use `actions/download-artifact` with that run ID to download the `ios-app` artifact. Upload to TestFlight as before. Add `actions: read` permission to the deploy job.

- **Cross-workflow artifact download**: `actions/download-artifact@v4` supports downloading from a different workflow run via `run-id` + `github-token`. The `GITHUB_TOKEN` with `actions: read` permission is sufficient — no PAT required.

- **Finding the latest run**: The GitHub API endpoint `GET /repos/{owner}/{repo}/actions/workflows/{workflow}/runs?status=success&branch={branch}&per_page=1` returns the most recent successful run. This call can be made with `gh api` or a `curl` step in the workflow using the default token.

## Testing Decisions

These are infrastructure changes to CI workflows, not application code. There is no application seam to test. Verification is behavioral:

- Trigger `android-build.yml` on a branch and confirm the uploaded artifact contains a signed AAB (verify with `apksigner verify` or by inspecting the artifact).
- Trigger `deploy-android.yml` via `workflow_dispatch` with no branch input and confirm it downloads from the `android-build.yml` artifact rather than rebuilding.
- Trigger `deploy-android.yml` via `workflow_dispatch` with an explicit branch and confirm it finds that branch's latest build.
- Confirm `deploy-ios.yml` downloads the `ios-app` artifact (renamed from `artifacts-ios`) from `macos-build.yml` without running a build.

## Out of Scope

- Changing deploy targets (Google Play internal track and TestFlight remain unchanged).
- Adding APK distribution outside of Google Play.
- Artifact retention policy changes (GitHub default 90-day expiry is acceptable).
- Windows or macOS Catalyst build/deploy workflows.
- Adding a run ID override input to the deploy workflows (can be added later if needed).
- Any changes to the app code, signing certificates, or provisioning profiles.

## Further Notes

- The Android build workflow currently sets keystore env vars (`KEYSTORE_BASE64`, `KEY_ALIAS`, `KEYSTORE_PASSWORD`, `KEY_PASSWORD`) that were only consumed by the now-removed bundletool step. These same vars will now be used for MAUI MSBuild signing — no new secrets are needed.
- Git tags can still be used for release tracking in git history; they just won't trigger deploys automatically.
- If a deploy is needed from a run older than 90 days, the artifact will have expired and a fresh build will be required.
