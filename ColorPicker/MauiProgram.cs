using ColorPicker.Services.Palette;
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

            // Register ViewModels
            builder.Services.AddTransient<MainViewModel>();
            builder.Services.AddSingleton<BottomNavViewModel>();
            builder.Services.AddSingleton<ColorCombinationsPanelViewModel>();
            builder.Services.AddTransient<ManualColorViewModel>();
            builder.Services.AddTransient<CameraPage>();
            builder.Services.AddTransient<ColorScanResultPage>();
            builder.Services.AddTransient<PalettePage>();
            builder.Services.AddTransient<PromptView>();

            // Register Views
            builder.Services.AddTransient<MainPage>();
            builder.Services.AddTransient<BottomNavBar>();
            builder.Services.AddTransient<ColorCombinationsPanel>();
            builder.Services.AddTransient<ManualColorPage>();
            builder.Services.AddTransient<ColorScanResultViewModel>();
            builder.Services.AddTransient<CameraViewModel>();
            builder.Services.AddTransient<PaletteViewModel>();
            builder.Services.AddTransient<PromptViewModel>();
            
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