using System.Windows.Input;
using ColorPicker.Models.Colors;
using ColorPicker.Services.Palette;

namespace ColorPicker.ViewModels;

public class ManualColorViewModel : BaseViewModel, IQueryAttributable
{
    public string PageTitle => IsEditMode ? "Edit Color" : "Pick a Color";
    public bool ShowCombinationsPanel => !IsEditMode;
    public bool ShowConfirmBar => IsEditMode;

    public bool IsEditMode
    {
        get;
        set
        {
            SetField(ref field, value);
            OnPropertyChanged(nameof(PageTitle));
            OnPropertyChanged(nameof(ShowCombinationsPanel));
            OnPropertyChanged(nameof(ShowConfirmBar));
        }
    }

    public string InitialHex { get; private set; } = string.Empty;

    // Current color
    public Color CurrentColor { get; private set; } = Colors.Transparent;
    public string HexInput { get; private set; } = string.Empty;

    // HSV sliders
    private double _hue;
    public double Hue
    {
        get => _hue;
        set
        {
            if (SetField(ref _hue, value)) SyncFromHsv();
        }
    }

    private double _saturation;
    public double Saturation
    {
        get => _saturation;
        set
        {
            if (SetField(ref _saturation, value)) SyncFromHsv();
        }
    }

    private double _value;
    public double Value
    {
        get => _value;
        set
        {
            if (SetField(ref _value, value)) SyncFromHsv();
        }
    }

    // RGB sliders
    private double _red;
    public double Red
    {
        get => _red;
        set
        {
            if (SetField(ref _red, value)) SyncFromRgb();
        }
    }

    private double _green;
    public double Green
    {
        get => _green;
        set
        {
            if (SetField(ref _green, value)) SyncFromRgb();
        }
    }

    private double _blue;
    public double Blue
    {
        get => _blue;
        set
        {
            if (SetField(ref _blue, value)) SyncFromRgb();
        }
    }

    // Color combinations bottom sheet
    public ColorCombinationsPanelViewModel? CombinationsPanel { get; }

    // Commands
    public ICommand ConfirmEditCommand { get; }
    public ICommand CancelCommand { get; }
    public ICommand CopyHexCommand { get; }
    public ICommand AddToPaletteCommand { get; }
    
    public ManualColorViewModel(IPaletteService paletteService)
    {
        CombinationsPanel = IPlatformApplication.Current?.Services.GetRequiredService<ColorCombinationsPanelViewModel>();
        if (CombinationsPanel == null)
            throw new NullReferenceException("Combination panel is null and must be defined in MauiApplication.cs");
        
        CopyHexCommand = new Command(_ => { Clipboard.Default.SetTextAsync(HexInput); });
        CancelCommand = new Command(_ => { ReturnFromPage(); });
        AddToPaletteCommand = new Command(_ => {
        {
            paletteService.AddColor(ColorSwatch.FromColor(CurrentColor));
            ReturnFromPage();
        } });
        ConfirmEditCommand = new Command(_ =>
        {
            if (string.IsNullOrEmpty(InitialHex)) return;
            var currentSwatch = ColorSwatch.FromColor(CurrentColor);
            var originalSwatch = paletteService.CurrentPalette?.Palette.FirstOrDefault(currentSwatch.ValueEquals);
            if (originalSwatch == null) return;
            
            paletteService.UpdateColor(originalSwatch, currentSwatch);
            ReturnFromPage();
        });
    }

    private void SyncFromHsv()
    {
        var color = Color.FromHsv((float)(_hue / 360.0), (float)_saturation, (float)_value);

        CurrentColor = color;
        HexInput = color.ToHex();
            
        _red = Math.Round(color.Red * 255.0);
        _green = Math.Round(color.Green * 255.0);
        _blue = Math.Round(color.Blue * 255.0);

        UpdateAllProperties(notifyHsv: false, notifyRgb: true);
    }

    private void SyncFromRgb()
    {
        var color = Color.FromRgb(_red / 255.0, _green / 255.0, _blue / 255.0);

        CurrentColor = color;
        HexInput = color.ToHex();
            
        _hue = color.GetHue() * 360.0; // Normalized to 0-360 range for display/sliders
        _saturation = GetHsvSaturation(color);
        _value = GetValue(color); // Correctly returns 0.0 - 1.0

        UpdateAllProperties(notifyHsv: true, notifyRgb: false);
    }

    public void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        if (query.TryGetValue("isEditMode", out var obj1) && obj1 is bool isEditMode) IsEditMode = isEditMode;
        if (query.TryGetValue("colorHex", out var obj2) && obj2 is string colorHex)
        {
            InitialHex = colorHex;
            var color = Color.FromArgb(InitialHex);
            
            CurrentColor = color;
            HexInput = color.ToHex();
            
            _hue = color.GetHue() * 360.0;
            _saturation = GetHsvSaturation(color);
            _value = GetValue(color);
                
            // Set initial scale to 0-255
            _red = Math.Round(color.Red * 255.0);
            _green = Math.Round(color.Green * 255.0);
            _blue = Math.Round(color.Blue * 255.0);
            
            UpdateAllProperties(notifyHsv: true, notifyRgb: true);
        }
    }

    private void UpdateAllProperties(bool notifyHsv, bool notifyRgb)
    {
        OnPropertyChanged(nameof(CurrentColor));
        OnPropertyChanged(nameof(HexInput));

        if (notifyHsv)
        {
            OnPropertyChanged(nameof(Hue));
            OnPropertyChanged(nameof(Saturation));
            OnPropertyChanged(nameof(Value));
        }

        if (notifyRgb)
        {
            OnPropertyChanged(nameof(Red));
            OnPropertyChanged(nameof(Green));
            OnPropertyChanged(nameof(Blue));
        }

        if (ShowCombinationsPanel) CombinationsPanel?.TargetColor = CurrentColor;
    }

    private static double GetValue(Color color) => Math.Max(color.Red, Math.Max(color.Green, color.Blue));
    
    private static double GetHsvSaturation(Color color)
    {
        double max = Math.Max(color.Red, Math.Max(color.Green, color.Blue));
        double min = Math.Min(color.Red, Math.Min(color.Green, color.Blue));
    
        if (max == 0) return 0;
    
        return (max - min) / max;
    }

    private static void ReturnFromPage() => Shell.Current.GoToAsync("..");
}
