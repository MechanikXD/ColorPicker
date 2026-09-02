using ColorPicker.Services.Navigation;

namespace ColorPicker;

public partial class AnimatedPage : ContentPage
{
    private const uint SLIDE_DURATION_MS = 200;
    private const uint FADE_DURATION_MS = 150;
    private const double FALLBACK_WIDTH = 420;

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await PlayIncomingAnimation();
    }

    private Task PlayIncomingAnimation()
    {
        var distance = Width > 0 ? Width : FALLBACK_WIDTH;

        return NavigationTracker.LastDirection switch
        {
            NavigationDirection.Forward => SlideInAsync(this, distance),
            NavigationDirection.Backward => SlideInAsync(this, -distance),
            NavigationDirection.Tab => this.FadeToAsync(1, FADE_DURATION_MS, Easing.CubicOut),
            _ => Task.CompletedTask
        };
    }

    private static async Task SlideInAsync(Page page, double startX)
    {
        page.TranslationX = startX;
        page.Opacity = 1;
        await page.TranslateToAsync(0, 0, SLIDE_DURATION_MS, Easing.CubicOut);
    }

    public static async Task SlideOutAsync(Page page)
    {
        var distance = page.Width > 0 ? page.Width : FALLBACK_WIDTH;
        await page.TranslateToAsync(distance, 0, SLIDE_DURATION_MS, Easing.CubicIn);
        page.TranslationX = 0;
        page.Opacity = 0;
    }
}