using System.Windows.Input;
using SkiaSharp;

namespace ColorPicker.ViewModels;

public class ColorScanResultViewModel : BaseViewModel, IQueryAttributable
{
    public ImageSource? ScannedImageSource
    {
        get;
        set => SetField(ref field, value);
    } = null;
    
    public byte[] ImageBytes
    {
        get;
        set => SetField(ref field, value);
    } = [];

    public Color SampledColor
    {
        get;
        set => SetField(ref field, value);
    } = Colors.Transparent;

    public string HexValue
    {
        get;
        set => SetField(ref field, value);
    } = string.Empty;

    public double Brightness
    {
        get;
        set => SetField(ref field, value);
    } = 50;

    public double Contrast
    {
        get;
        set => SetField(ref field, value);
    } = 50;

    public double Red
    {
        get;
        set => SetField(ref field, value);
    } = 50;

    public double Green
    {
        get;
        set => SetField(ref field, value);
    } = 50;

    public double Blue
    {
        get;
        set => SetField(ref field, value);
    } = 50;

    public ColorCombinationsPanelViewModel CombinationsPanel { get; } = new();

    public ICommand RetakeCommand { get; }

    public ICommand SaveToPaletteCommand { get; }

    public ColorScanResultViewModel()
    {
        RetakeCommand = new Command(_ => { Shell.Current.GoToAsync(".."); });
        SaveToPaletteCommand = new Command(_ =>
        {
            /* add SampledColor to active palette */
        });
    }

    public void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        if (query.TryGetValue("CapturedImageBytes", out var obj) && obj is byte[] imageBytes)
        {
            ImageBytes = imageBytes;
            ScannedImageSource = ImageSource.FromStream(() => new MemoryStream(imageBytes));
        }
        
        using var bitmap = SKBitmap.Decode(ImageBytes);
        if (bitmap == null)
        {
            SampledColor = Colors.Transparent;
            return;
        }

        var color = bitmap.GetPixel(bitmap.Width / 2, bitmap.Height / 2);
        SampledColor = new Color(color.Red, color.Green, color.Blue);
    }
}