using System.Windows.Input;
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

        FlipCameraCommand = new Command(_ =>
        {
            IsFrontCamera = !IsFrontCamera;
            // wire to CameraView.CameraPosition binding
        });
    }

    private static async void PassImage(CameraView view)
    {
        if (view == null)
        {
            Console.WriteLine("PROGRAMMER LOG: Camera view is null");
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
            
        Console.WriteLine("PROGRAMMER LOG: Passing an image to the result view");
        await Shell.Current.GoToAsync("scanresult", navigationParameters);
    }
}
