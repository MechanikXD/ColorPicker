using ColorPicker.Services.History;
using ColorPicker.Services.Localization;
using ColorPicker.Services.Palette;
using ColorPicker.Services.SaveLoad;
using ColorPicker.Services.Theme;
using ColorPicker.View;
using ColorPicker.ViewModels;
using CommunityToolkit.Maui;
using Microsoft.Extensions.Logging;
using MainViewModel = ColorPicker.ViewModels.MainViewModel;

namespace ColorPicker;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        try
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>().UseMauiCommunityToolkit().UseMauiCommunityToolkitCamera()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });
        
            // Register the Shell
            builder.Services.AddSingleton<AppShell>();
        
            // Register Services
            builder.Services.AddSingleton<IThemeService, ThemeService>();
            builder.Services.AddSingleton<IPaletteService, PaletteService>();
            builder.Services.AddSingleton<IHistoryService, HistoryService>();
            builder.Services.AddSingleton<ISaveLoadService, PaletteSaveLoadService>();
            builder.Services.AddSingleton<ISaveLoadService, LocalizationSaveLoadService>();
            builder.Services.AddSingleton<ISaveLoadService, HistorySaveLoadService>();

            // Register ViewModels
            builder.Services.AddSingleton<MainViewModel>();
            builder.Services.AddSingleton<CameraPage>();
            builder.Services.AddSingleton<PalettePage>();
            builder.Services.AddSingleton<HistoryPage>();
            
            builder.Services.AddSingleton<ManualColorViewModel>();
            builder.Services.AddSingleton<ColorScanResultPage>();
            builder.Services.AddSingleton<BottomNavViewModel>();
            builder.Services.AddSingleton<ColorCombinationsPanelViewModel>();
            builder.Services.AddSingleton<PromptView>();

            // Register Views
            builder.Services.AddSingleton<MainPage>();
            builder.Services.AddSingleton<PaletteViewModel>();
            builder.Services.AddSingleton<CameraViewModel>();
            builder.Services.AddSingleton<HistoryViewModel>();
            
            builder.Services.AddSingleton<ManualColorPage>();
            builder.Services.AddSingleton<ColorScanResultViewModel>();
            builder.Services.AddSingleton<BottomNavBar>();
            builder.Services.AddSingleton<ColorCombinationsPanel>();
            builder.Services.AddSingleton<PromptViewModel>();
            
#if DEBUG
            builder.Logging.AddDebug();
#endif
            
            return builder.Build();
        }
        catch(Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"MAUI CRASH ERROR: {ex.Message}");
            System.Diagnostics.Debug.WriteLine($"STACK TRACE: {ex.StackTrace}");
            throw;
        }
    }
}