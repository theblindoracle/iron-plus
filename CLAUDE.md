# CLAUDE.md

This file provides guidance to Claude Code (claude.ai/code) when working with code in this repository.

## Build Commands

```bash
# Restore dependencies
dotnet restore IronPlus/IronPlus.csproj

# Build for Android
dotnet build IronPlus/IronPlus.csproj -c Release -f:net10.0-android

# Build for iOS (simulator)
dotnet build IronPlus/IronPlus.csproj -f net10.0-ios -c Debug /p:packageApp=false /p:buildForSimulator=true

# Clean bin/obj directories
./scripts/clean.sh
```

## Architecture

IronPlus is a .NET 10 MAUI cross-platform strength training calculator app (iOS, Android).

### MVVM Pattern with TinyIoC

The app uses MVVM with a custom TinyIoC dependency injection container configured in `ViewModelLocator.cs`:

- **ViewModels** are registered as multi-instance
- **Services** are registered as singletons via interfaces
- Views auto-wire to ViewModels via the `AutoWireViewModel` attached property (naming convention: `FooPage` → `FooViewModel`)

```xml
<!-- Example in XAML -->
viewmodels:ViewModelLocator.AutoWireViewModel="true"
```

### Key Services

| Interface | Implementation | Purpose |
|-----------|----------------|---------|
| `IRpeCalculationService` | `RpeCalculationService` | RPE-based 1RM calculations using Tuchscherer's algorithm |
| `IWarmUpCalculationService` | `WarmUpCalculationService` | Warm-up set generation |
| `IDatabaseService` | `DatabaseService` | SQLite CRUD operations (barbells) |
| `ISettingsService` | `SettingsService` | User preferences via `Preferences` API |
| `IThemeService` | `ThemeService` | Light/dark theme management |
| `IDialogService` | `DialogService` | User dialogs (Acr.UserDialogs) |
| `IPlatformService` | `PlatformService` (per-platform) | Platform-specific operations (e.g. status bar color) |

`AnalyticsService` is a static class (not injected) that wraps Sentry SDK calls for page views, events, and exception tracking.

### Navigation

Shell-based navigation defined in `AppShell.xaml` with 4 main tabs:
- RPE Calculator
- Unit Converter
- Warm-up Calculator
- Settings

### Data Storage

- **SQLite**: `IronPlus.db3` in `LocalApplicationData` folder (stores Barbells)
- **Preferences**: User settings via .NET MAUI Preferences API

### Validation

Custom validation framework in `/Validation/`:
- `ValidatableObject<T>` wrapper for validatable properties
- Rules implement `IValidationRule<T>` interface

### Third-Party Dependencies

- **Syncfusion.Maui.Buttons/Inputs** - UI controls (requires license key in `MauiProgram.cs`)
- **sqlite-net-pcl** - SQLite ORM
- **Acr.UserDialogs** - Cross-platform dialogs
- **Newtonsoft.Json** - JSON serialization
- **Sentry.Maui** - Crash reporting and analytics (replaces AppCenter)
- **CommunityToolkit.Mvvm** - MVVM source generators and helpers

## Target Frameworks

- `net10.0-android` (API 21+)
- `net10.0-ios` (iOS 12.2+)

The `ApplicationId` differs per platform (see `IronPlus.csproj`): `com.ironplusdev.ironplus` on Android, `com.napesdotnet.ironplus` on iOS.

## CI/CD

GitHub Actions workflows are split into build (automatic) and deploy (manual) pairs, so the artifact that ships is exactly what CI built and verified — deploys never rebuild:

| Workflow | Trigger | Purpose |
|----------|---------|---------|
| `.github/workflows/android-build.yml` | push to `main`, PRs | Publishes and signs the AAB, uploads as `android-app` artifact |
| `.github/workflows/deploy-android.yml` | manual (`workflow_dispatch`, branch input) | Downloads the latest successful `android-app` build artifact and uploads to Google Play (internal track) via Workload Identity Federation |
| `.github/workflows/macos-build.yml` | push/PR to `main`, manual | Builds and signs the iOS IPA, uploads as `ios-app` artifact |
| `.github/workflows/deploy-ios.yml` | manual (`workflow_dispatch`, branch input) | Downloads the latest successful `ios-app` build artifact and uploads to TestFlight |

Required secrets/variables for these workflows are tracked in `TODO.md`. See `specs/separate-build-deploy-workflows.md` for the rationale behind the build/deploy split.
