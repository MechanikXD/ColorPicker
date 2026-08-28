namespace ColorPicker.Services.Theme;

public interface IThemeService
{
    ApplicationTheme CurrentTheme { get; }
    void SetTheme(ApplicationTheme newTheme);
}