# CLAUDE.md

This file provides guidance to Claude Code (claude.ai/code) when working with code in this repository.

## Build Commands

```bash
# Restore dependencies
dotnet restore IronPlus/IronPlus.csproj

# Build for Android
dotnet build IronPlus/IronPlus.csproj -c Release -f:net8.0-android

# Build for iOS (simulator)
dotnet build IronPlus/IronPlus.csproj -f net8.0-ios -c Debug /p:packageApp=false /p:buildForSimulator=true

# Build for macOS
dotnet build IronPlus/IronPlus.csproj -f net8.0-maccatalyst -c Debug

# Clean bin/obj directories
./scripts/clean.sh
```

## Architecture

IronPlus is a .NET 8 MAUI cross-platform strength training calculator app (iOS, Android, macOS, Windows).

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

## Target Frameworks

- `net8.0-android` (API 21+)
- `net8.0-ios` (iOS 11.0+)
- `net8.0-maccatalyst` (macOS 13.1+)
- `net8.0-windows10.0.19041.0`
