using ColorPicker.Services.Theme;
using ColorPicker.View;
using ColorPicker.ViewModels;
using CommunityToolkit.Maui;
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
                .UseMauiCommunityToolkit().UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });
        
            // Register the Shell
            builder.Services.AddSingleton<AppShell>();
        
            // Register Services
            builder.Services.AddSingleton<IThemeService, ThemeService>();

            // Register ViewModels
            builder.Services.AddTransient<MainViewModel>();
            builder.Services.AddSingleton<BottomNavViewModel>();
            builder.Services.AddTransient<ColorCombinationsPanelViewModel>();
            builder.Services.AddTransient<ManualColorViewModel>();

            // Register Views
            builder.Services.AddTransient<MainPage>();
            builder.Services.AddTransient<BottomNavBar>();
            builder.Services.AddTransient<ColorCombinationsPanel>();
            builder.Services.AddTransient<ManualColorPage>();
            
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