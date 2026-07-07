# iOS Code Signing Setup

Instructions for configuring Apple code signing for the iOS CI workflow to produce an App Store / TestFlight `.ipa`.

## Step 1 — Export the Distribution Certificate (P12)

1. Open **Keychain Access** on your Mac
2. Find `Apple Distribution: Your Name (TEAMID)` under **My Certificates**
3. Right-click → **Export** → choose `.p12` format and set a password
4. Convert to base64:
   ```bash
   base64 -i certificate.p12 | pbcopy
   ```

If the certificate doesn't exist yet, create one at [developer.apple.com](https://developer.apple.com) → Certificates → `+` → **Apple Distribution**.

## Step 2 — Download the App Store Provisioning Profile

1. Go to [developer.apple.com](https://developer.apple.com) → **Profiles** → `+`
2. Choose **App Store Connect** distribution
3. Select App ID `com.napesdotnet.ironplus` and your Distribution certificate
4. Download the `.mobileprovision` file
5. Convert to base64:
   ```bash
   base64 -i IronPlus_AppStore.mobileprovision | pbcopy
   ```
6. Extract the UUID:
   ```bash
   security cms -D -i IronPlus_AppStore.mobileprovision \
     | grep -A1 UUID | grep string \
     | sed 's/.*<string>\(.*\)<\/string>/\1/'
   ```

## Step 3 — Find the Signing Identity Name

After the certificate is in your keychain, run:

```bash
security find-identity -v -p codesigning | grep "Apple Distribution"
```

Copy the full string, e.g. `Apple Distribution: Your Name (XXXXXXXXXX)`.

## Step 4 — Add GitHub Secrets

Go to **Settings → Secrets and variables → Actions → New repository secret** and add each of the following:

| Secret | Value |
|--------|-------|
| `BUILD_CERTIFICATE_BASE64` | base64-encoded `.p12` from Step 1 |
| `P12_PASSWORD` | password set on the `.p12` |
| `BUILD_PROVISION_PROFILE_BASE64` | base64-encoded `.mobileprovision` from Step 2 |
| `PROVISIONING_PROFILE_UUID` | UUID extracted in Step 2 |
| `SIGNING_IDENTITY` | full cert name from Step 3, e.g. `Apple Distribution: Name (TEAMID)` |
| `KEYCHAIN_PASSWORD` | any random string — generate one with `openssl rand -hex 16` |
| `SENTRY_AUTH_TOKEN` | Sentry auth token with `project:releases` and `org:read` scopes |
| `SYNCFUSION_KEY` | Syncfusion license key |
| `SENTRY_DSN` | Sentry DSN for the project |
