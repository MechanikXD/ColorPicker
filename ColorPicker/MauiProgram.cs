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
            builder.Services.AddSingleton<ISaveLoadService, PaletteSaveLoadService>();

            // Register ViewModels
            //  Main pages - singletons
            builder.Services.AddSingleton<MainViewModel>();
            builder.Services.AddSingleton<CameraPage>();
            builder.Services.AddSingleton<PalettePage>();
            
            builder.Services.AddTransient<ManualColorViewModel>();
            builder.Services.AddTransient<ColorScanResultPage>();
            builder.Services.AddSingleton<BottomNavViewModel>();
            builder.Services.AddSingleton<ColorCombinationsPanelViewModel>();
            builder.Services.AddTransient<PromptView>();

            // Register Views
            //  Main page's ViewModels are singletons as well
            builder.Services.AddSingleton<MainPage>();
            builder.Services.AddSingleton<PaletteViewModel>();
            builder.Services.AddSingleton<CameraViewModel>();
            
            builder.Services.AddTransient<ColorCombinationsPanel>();
            builder.Services.AddTransient<ManualColorPage>();
            builder.Services.AddTransient<ColorScanResultViewModel>();
            builder.Services.AddTransient<BottomNavBar>();
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