using System.Windows.Input;

namespace ColorPicker.ViewModels;

public class ManualColorViewModel : BaseViewModel
{
    private Color _currentColor = Colors.Transparent;
    private double _hue;
    private double _saturation = 1;
    private double _value = 1;
    private double _red;
    private double _green;
    private double _blue;
    private string _hexInput = string.Empty;
    private double _brightness;
    private double _contrastOnWhite;
    private double _contrastOnBlack;

    // Current color
    public Color CurrentColor { get => _currentColor; set => SetField(ref _currentColor, value); }
    public string HexInput { get => _hexInput; set => SetField(ref _hexInput, value); }

    // HSV sliders
    public double Hue { get => _hue; set => SetField(ref _hue, value); }
    public double Saturation { get => _saturation; set => SetField(ref _saturation, value); }
    public double Value { get => _value; set => SetField(ref _value, value); }

    // RGB sliders
    public double Red { get => _red; set => SetField(ref _red, value); }
    public double Green { get => _green; set => SetField(ref _green, value); }
    public double Blue { get => _blue; set => SetField(ref _blue, value); }

    // Derived readouts (computed from CurrentColor, updated on change)
    public double Brightness { get => _brightness; set => SetField(ref _brightness, value); }
    public double ContrastOnWhite { get => _contrastOnWhite; set => SetField(ref _contrastOnWhite, value); }
    public double ContrastOnBlack { get => _contrastOnBlack; set => SetField(ref _contrastOnBlack, value); }

    // Color combinations bottom sheet
    public ColorCombinationsPanelViewModel CombinationsPanel { get; } = new();

    // Commands
    public ICommand ConfirmColorCommand { get; }
    public ICommand CancelCommand { get; }
    public ICommand CopyHexCommand { get; }
    public ICommand AddToPaletteCommand { get; }

    public ManualColorViewModel()
    {
        ConfirmColorCommand = new Command(_ => { /* pop and return CurrentColor to caller */ });
        CancelCommand = new Command(_ => { /* Shell.Current.GoToAsync("..") */ });
        CopyHexCommand = new Command(_ => { /* Clipboard.Default.SetTextAsync(HexInput) */ });
        AddToPaletteCommand = new Command(_ => { /* add CurrentColor to active palette */ });
    }
}
