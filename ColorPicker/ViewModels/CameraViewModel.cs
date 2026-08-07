using System.Windows.Input;
using ColorPicker.Services.Navigation;
using CommunityToolkit.Maui.Views;

namespace ColorPicker.ViewModels;

public class CameraViewModel : BaseViewModel
{
    private bool _isCapturing;
    public bool IsFrontCamera
    {
        get;
        set => SetField(ref field, value);
    } = false;

    public ICommand CaptureCommand { get; }

    public ICommand FlipCameraCommand { get; }
    
    public CameraViewModel()
    {
        CaptureCommand = new Command<CameraView>(PassImage);
        FlipCameraCommand = new Command<CameraView>(FlipCamera);
    }

    private async void FlipCamera(CameraView? view)
    {
        if (view == null) return;

        var cameras = await view.GetAvailableCameras(CancellationToken.None);
        if (cameras.Any())
        {
            var current = view.SelectedCamera;
            if (current == null) return;
            
            var next = current.Equals(cameras[0]) 
                ? cameras.Skip(1).FirstOrDefault() 
                : cameras[0];
        
            if (next != null)
            {
                view.SelectedCamera = next;
                IsFrontCamera = !IsFrontCamera;
            }
        }
    }

    private async void PassImage(CameraView? view)
    {
        if (_isCapturing) return;
        _isCapturing = true;
        if (view == null)
        {
            await ShellNavigationService.GoBackAsync();
            return;
        }
        
        await using var imageStream = await view.CaptureImage(CancellationToken.None);
        
        using MemoryStream memoryStream = new();
        await imageStream.CopyToAsync(memoryStream);
        var imageBytes = memoryStream.ToArray();
            
        var navigationParameters = new Dictionary<string, object>
        {
            { "CapturedImageBytes", imageBytes }
        };
        
        await ShellNavigationService.GoToPageWithParamsAsync("scanresult", navigationParameters);
        _isCapturing = false;
    }
}
