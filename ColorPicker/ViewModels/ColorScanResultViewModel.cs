using System.Windows.Input;
using ColorPicker.Models.Colors;
using ColorPicker.Services.Navigation;
using ColorPicker.Services.Palette;
using SkiaSharp;

namespace ColorPicker.ViewModels;

public class ColorScanResultViewModel : BaseViewModel, IQueryAttributable
{
    private SKBitmap? _imageBitmap;
    private CancellationTokenSource? _sliderDebounceCancellationTokenSource;
    private const int DEBOUNCE_DELAY_MILLISECONDS = 100;
    
    public ImageSource? ScannedImageSource
    {
        get;
        set => SetField(ref field, value);
    } = null;

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
        set { if (SetField(ref field, value)) OnSliderValueChanged(); }
    } = 50;

    public double Contrast
    {
        get;
        set { if (SetField(ref field, value)) OnSliderValueChanged(); }
    } = 50;
    
    public double Saturation
    {
        get;
        set { if (SetField(ref field, value)) OnSliderValueChanged(); }
    } = 50;

    public double Red
    {
        get;
        set { if (SetField(ref field, value)) OnSliderValueChanged(); }
    } = 128;

    public double Green
    {
        get;
        set { if (SetField(ref field, value)) OnSliderValueChanged(); }
    } = 128;

    public double Blue
    {
        get;
        set { if (SetField(ref field, value)) OnSliderValueChanged(); }
    } = 128;

    public ColorCombinationsPanelViewModel? CombinationsPanel { get; }
    public PromptViewModel Prompt { get; }

    public ICommand RetakeCommand { get; }
    public ICommand SaveToPaletteCommand { get; }

    public ColorScanResultViewModel(IPaletteService paletteService, ColorCombinationsPanelViewModel colorCombinationsPanel, PromptViewModel prompt)
    {
        CombinationsPanel = colorCombinationsPanel;
        Prompt = prompt;
        
        RetakeCommand = new Command(_ => { ShellNavigationService.GoToPage("camera"); });
        SaveToPaletteCommand = new Command(_ =>
        {
            Prompt.Show(
                title: "Enter color title",
                message: "Name your color so you can find it later",
                inputHint: "Color's name",
                showInput: true,
                onConfirm: () =>
                {
                    var title = string.IsNullOrEmpty(Prompt.InputText) || string.IsNullOrWhiteSpace(Prompt.InputText)
                        ? SampledColor.ToHex()
                        : Prompt.InputText;
                    paletteService.AddColor(ColorSwatch.FromColor(SampledColor, name: title));
                }
            );
        });
    }

    public void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        if (query.TryGetValue("CapturedImageBytes", out var obj) && obj is byte[] imageBytes)
        {
            _imageBitmap = CorrectBitmapOrientation(imageBytes);
            ScannedImageSource = ImageSource.FromStream(() => new MemoryStream(imageBytes));
        }

        if (_imageBitmap == null) return;
        UpdateSampledColor(_imageBitmap);
    }
    
    private SKBitmap CorrectBitmapOrientation(byte[] rawBytes)
    {
        // Use SKCodec to read metadata embedded by Android
        using var stream = new MemoryStream(rawBytes);
        using var codec = SKCodec.Create(stream);
        if (codec == null) return SKBitmap.Decode(rawBytes);

        var origin = codec.EncodedOrigin;
        var originalBitmap = SKBitmap.Decode(rawBytes);

        // Evaluate the native hardware orientation tag
        return origin switch
        {
            SKEncodedOrigin.RightTop => // Rotated 90 Degrees Clockwise
                RotateBitmap(originalBitmap, 90),
            SKEncodedOrigin.BottomRight => // Rotated 180 Degrees
                RotateBitmap(originalBitmap, 180),
            SKEncodedOrigin.LeftBottom => // Rotated 90 Degrees CounterClockwise
                RotateBitmap(originalBitmap, 270),
            _ => originalBitmap
        };
    }
    
    private void OnSliderValueChanged()
    {
        _sliderDebounceCancellationTokenSource?.Cancel();
        _sliderDebounceCancellationTokenSource = new CancellationTokenSource();
        var token = _sliderDebounceCancellationTokenSource.Token;
        Task.Delay(DEBOUNCE_DELAY_MILLISECONDS, token).ContinueWith(_ =>
        {
            if (!token.IsCancellationRequested)
            {
                MainThread.BeginInvokeOnMainThread(UpdateImage);
            }
        }, token);
    }
    
    private void UpdateImage()
    {
        if (_imageBitmap == null) return;
        using var adjustedBitmap = new SKBitmap(_imageBitmap.Width, _imageBitmap.Height);
        using var canvas = new SKCanvas(adjustedBitmap);

        var matrix = GetAdjustmentsMatrix();

        // Render the image
        using var paint = new SKPaint();
        paint.ColorFilter = SKColorFilter.CreateColorMatrix(matrix);
        canvas.DrawBitmap(_imageBitmap, 0, 0, paint);

        UpdateSampledColor(adjustedBitmap);
        
        // Output back to the UI
        using var image = SKImage.FromBitmap(adjustedBitmap);
        using var data = image.Encode(SKEncodedImageFormat.Jpeg, 85);
        
        using var stream = new MemoryStream();
        data.SaveTo(stream);
        var buffer = stream.ToArray();
        ScannedImageSource = ImageSource.FromStream(() => new MemoryStream(buffer));
    }

    private void UpdateSampledColor(SKBitmap bitmap)
    {
        var centerPixel = bitmap.GetPixel(bitmap.Width / 2, bitmap.Height / 2);
        SampledColor = Color.FromRgb(centerPixel.Red, centerPixel.Green, centerPixel.Blue);
        HexValue = SampledColor.ToHex();
        CombinationsPanel?.TargetColor = SampledColor;
    }
    
    private float[] GetAdjustmentsMatrix ()
    {
        var a = GetBrightnessContrastMatrix();
        var b = GetSaturationMatrix();
        var result = new float[20];
        for (var row = 0; row < 4; row++)
        {
            for (var col = 0; col < 5; col++)
            {
                var index = row * 5 + col;
                if (col == 4)
                    result[index] = a[row * 5 + 0] * b[4] + a[row * 5 + 1] * b[9] + a[row * 5 + 2] * b[14] +
                                    a[row * 5 + 3] * b[19] + a[row * 5 + 4];
                else
                    result[index] = a[row * 5 + 0] * b[0 * 5 + col] + a[row * 5 + 1] * b[1 * 5 + col] +
                                    a[row * 5 + 2] * b[2 * 5 + col] + a[row * 5 + 3] * b[3 * 5 + col];
            }
        }
        
        result[4]  += (float)((Red - 128) / 255.0);   // Red offset
        result[9]  += (float)((Green - 128) / 255.0); // Green offset
        result[14] += (float)((Blue - 128) / 255.0);  // Blue offset

        return result;
    }

    private SKBitmap RotateBitmap(SKBitmap bitmap, int degrees)
    {
        if (degrees == 0) return bitmap;

        var is90Or270 = degrees is 90 or 270;
        var width = is90Or270 ? bitmap.Height : bitmap.Width;
        var height = is90Or270 ? bitmap.Width : bitmap.Height;

        var rotatedBitmap = new SKBitmap(width, height);
        using (var canvas = new SKCanvas(rotatedBitmap))
        {
            canvas.Translate(width / 2f, height / 2f);
            canvas.RotateDegrees(degrees);
            canvas.Translate(-bitmap.Width / 2f, -bitmap.Height / 2f);
            canvas.DrawBitmap(bitmap, 0, 0);
        }
    
        bitmap.Dispose(); // Free unmanaged memory of the old wrong layout
        return rotatedBitmap;
    }
    
    private float[] GetBrightnessContrastMatrix()
    {
        var c = (float)(Contrast / 50.0);
        var b = (float)(Brightness / 50.0);
        
        var scale = c * b;
        var translate = (1.0f - c) / 2.0f;

        return
        [
            scale, 0, 0, 0, translate,
            0, scale, 0, 0, translate,
            0, 0, scale, 0, translate,
            0, 0, 0, 1, 0
        ];
    }

    private float[] GetSaturationMatrix()
    {
        // Map slider (0-100, default 50) to a saturation factor (0.0 = Grayscale, 1.0 = Normal, 2.0 = Ultra Vivid)
        var s = (float)(Saturation / 50.0);

        // Standard NTSC luminance weights for human eye color perception
        const float R_WEIGHT = 0.299f;
        const float G_WEIGHT = 0.587f;
        const float B_WEIGHT = 0.114f;

        var rInv = 1.0f - s;

        return
        [
            rInv * R_WEIGHT + s, rInv * G_WEIGHT, rInv * B_WEIGHT, 0, 0,
            rInv * R_WEIGHT, rInv * G_WEIGHT + s, rInv * B_WEIGHT, 0, 0,
            rInv * R_WEIGHT, rInv * G_WEIGHT, rInv * B_WEIGHT + s, 0, 0,
            0, 0, 0, 1, 0
        ];
    }
}