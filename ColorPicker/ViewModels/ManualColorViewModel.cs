using System.Windows.Input;

namespace ColorPicker.ViewModels;

public class ManualColorViewModel : BaseViewModel
{
    // Current color
    public Color CurrentColor
    {
        get;
        set => SetField(ref field, value);
    } = Colors.Transparent;

    public string HexInput
    {
        get;
        set => SetField(ref field, value);
    } = string.Empty;

    // HSV sliders
    public double Hue
    {
        get;
        set => SetField(ref field, value);
    }

    public double Saturation
    {
        get;
        set => SetField(ref field, value);
    } = 1;

    public double Value
    {
        get;
        set => SetField(ref field, value);
    } = 1;

    // RGB sliders
    public double Red
    {
        get;
        set => SetField(ref field, value);
    }

    public double Green
    {
        get;
        set => SetField(ref field, value);
    }

    public double Blue
    {
        get;
        set => SetField(ref field, value);
    }

    // Derived readouts (computed from CurrentColor, updated on change)
    public double Brightness
    {
        get;
        set => SetField(ref field, value);
    }

    public double ContrastOnWhite
    {
        get;
        set => SetField(ref field, value);
    }

    public double ContrastOnBlack
    {
        get;
        set => SetField(ref field, value);
    }

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
        CancelCommand = new Command(_ => { Shell.Current.GoToAsync(".."); });
        CopyHexCommand = new Command(_ => { Clipboard.Default.SetTextAsync(HexInput); });
        AddToPaletteCommand = new Command(_ => { /* add CurrentColor to active palette */ });
    }
}
