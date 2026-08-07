using System.Windows.Input;
using ColorPicker.Services.Navigation;
using CommunityToolkit.Maui.Views;

namespace ColorPicker.ViewModels;

public class CameraViewModel : BaseViewModel
{
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

    private static async void PassImage(CameraView? view)
    {
        if (view == null)
        {
            await ShellNavigationService.StepBackAsync();
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
        
        await ShellNavigationService.SwitchSubPageAsync("scanresult", navigationParameters);
    }
}
