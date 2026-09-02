using ColorPicker.Services.Navigation;
using ColorPicker.ViewModels;

namespace ColorPicker.View;

public partial class CameraPage : AnimatedPage
{
    public CameraPage(CameraViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
        viewModel.OnCaptureStarted += async (_, _) => await CameraCaptureFeedback();
        viewModel.OnCaptureFinished += (_, _) => ToggleLoadingOverlay(true);
    }
    
    protected override async void OnAppearing()
    {
        base.OnAppearing();
        if (!await HasValidCameraPermission()) await ShellNavigationService.GoBackAsync();
        ToggleLoadingOverlay(false);
    }

    private async void OnCameraViewLoaded(object? sender, EventArgs e)
    {
        base.OnAppearing();
        await LiveCamera.StartCameraPreview(CancellationToken.None);
    }

    private void OnCameraViewUnloaded(object? sender, EventArgs e)
    {
        base.OnDisappearing();
        LiveCamera.StopCameraPreview();
    }
    
    public async Task<bool> HasValidCameraPermission()
    {
        var permissionStatus = await Permissions.CheckStatusAsync<Permissions.Camera>();
        if (permissionStatus is PermissionStatus.Disabled or PermissionStatus.Restricted or PermissionStatus.Denied)
        {
            var accessPermissionStatus = await Permissions.RequestAsync<Permissions.Camera>();
            if (accessPermissionStatus != PermissionStatus.Granted) return false;
        }

        return true;
    }
    
    private async Task CameraCaptureFeedback()
    {
        FlashOverlay.IsVisible = true;
        await FlashOverlay.FadeToAsync(1, 60);
        await FlashOverlay.FadeToAsync(0, 120);
        FlashOverlay.IsVisible = false;

        ToggleLoadingOverlay(true);
    }

    private void ToggleLoadingOverlay(bool toggle)
    {
        LoadingOverlay.IsVisible = toggle;
        CameraPointer.IsVisible = !toggle;
    }
}