using ColorPicker.ViewModels;

namespace ColorPicker.View;

public partial class CameraPage : ContentPage
{
    public CameraPage(CameraViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
    
    protected override async void OnAppearing()
    {
        base.OnAppearing();

        var permissionStatus = await Permissions.CheckStatusAsync<Permissions.Camera>();
        if (permissionStatus is PermissionStatus.Disabled or PermissionStatus.Restricted or PermissionStatus.Denied)
        {
            var accessPermissionStatus = await Permissions.RequestAsync<Permissions.Camera>();
            if (accessPermissionStatus != PermissionStatus.Granted)
            {
                await Shell.Current.GoToAsync("..");
            }
        }
    }

    private async void OnCameraViewLoaded(object? sender, EventArgs e)
    {
        base.OnAppearing();
        await LiveCamera.StartCameraPreview(CancellationToken.None);
    }
    
    protected override void OnDisappearing()
    {
        LiveCamera.StopCameraPreview();
        base.OnDisappearing();
    }
}