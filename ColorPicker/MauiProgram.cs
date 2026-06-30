using ColorPicker.Models;
using ColorPicker.View;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Maui.Hosting;
using MainViewModel = ColorPicker.ViewModels.MainViewModel;

namespace ColorPicker;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder.ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
            });

        // Register ViewModels
        builder.Services.AddTransient<MainViewModel>();

        // Register Views
        builder.Services.AddTransient<MainPage>();

        return builder.Build();
    }
}