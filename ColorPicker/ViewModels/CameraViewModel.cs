using System.Windows.Input;
using ColorPicker.Models.StaticData;
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

    public event EventHandler? OnCaptureStarted;
    public event EventHandler? OnCaptureFinished;

    public ICommand CaptureCommand { get; }

    public ICommand FlipCameraCommand { get; }
    
    public CameraViewModel()
    {
        CaptureCommand = new Command<CameraView>(async void (view) => { await PassImage(view); });
        FlipCameraCommand = new Command<CameraView>(async void (view) => { await FlipCamera(view); });
    }

    private async Task FlipCamera(CameraView? view)
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

    private async Task PassImage(CameraView? view)
    {
        if (_isCapturing) return;
        _isCapturing = true;
        if (view == null)
        {
            await ShellNavigationService.GoBackAsync();
            return;
        }
        
        OnCaptureStarted?.Invoke(this, EventArgs.Empty);
        using var cts = new CancellationTokenSource(TimeSpan.FromSeconds(5));

        try
        {
            var imageStream = await view.CaptureImage(cts.Token);
            using MemoryStream memoryStream = new();
            await imageStream.CopyToAsync(memoryStream, cts.Token);
            var imageBytes = memoryStream.ToArray();

            OnCaptureFinished?.Invoke(this, EventArgs.Empty);
            await NavigateToColorScanResult(imageBytes);
        }
        catch (OperationCanceledException)
        {
            // TODO: Notify user of operation timeout.
        }
        
        
        _isCapturing = false;
    }

    private static async Task NavigateToColorScanResult(byte[] image)
    {
        var navigationParameters = new Dictionary<string, object> { [QueryAttributes.IMAGE_BYTES] = image };
        await ShellNavigationService.GoToSubPageAsync(Pages.Sub.ColorScanResult, navigationParameters);
    }
}
