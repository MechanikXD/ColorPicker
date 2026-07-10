using System.Windows.Input;
using ColorPicker.Models.Colors;
using ColorPicker.Services.Palette;

namespace ColorPicker.ViewModels;

public class ManualColorViewModel : BaseViewModel, IQueryAttributable
{
    public string PageTitle => IsEditMode ? "Edit Color" : "Pick a Color";
    public bool ShowCombinationsPanel => !IsEditMode;
    public bool ShowConfirmBar => IsEditMode;
    
    private readonly IPaletteService _paletteService;
    private bool _isEditMode;
    private string _initialHex = string.Empty;
    
    public bool IsEditMode
    {
        get => _isEditMode;
        set
        {
            SetField(ref _isEditMode, value);
            OnPropertyChanged(nameof(PageTitle));
            OnPropertyChanged(nameof(ShowCombinationsPanel));
            OnPropertyChanged(nameof(ShowConfirmBar));
        }
    }
 
    public string InitialHex
    {
        get => _initialHex;
        set
        {
            SetField(ref _initialHex, value);
            // TODO: parse hex and pre-load CurrentColor / sliders
        }
    }
    
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

    // Color combinations bottom sheet
    public ColorCombinationsPanelViewModel CombinationsPanel { get; } = new();

    // Commands
    public ICommand ConfirmEditCommand { get; }
    public ICommand CancelCommand { get; }
    public ICommand CopyHexCommand { get; }
    public ICommand AddToPaletteCommand { get; }

    public ManualColorViewModel(IPaletteService paletteService)
    {
        _paletteService = paletteService;

        CopyHexCommand = new Command(_ => { Clipboard.Default.SetTextAsync(HexInput); });
        CancelCommand = new Command(_ => { Shell.Current.GoToAsync(".."); });
        AddToPaletteCommand = new Command(_ => { _paletteService.AddColor(ColorSwatch.FromColor(CurrentColor)); });
        ConfirmEditCommand = new Command(_ =>
        {
            // _paletteService.UpdateColor(originalSwatch, ColorSwatch.FromColor(CurrentColor));
            Shell.Current.GoToAsync("..");
        });
    }

    public void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        if (query.TryGetValue("isEditMode", out var obj1) && obj1 is bool isEditMode) _isEditMode = isEditMode;
        if (query.TryGetValue("colorHex", out var obj2) && obj2 is string colorHex) _initialHex = colorHex;
    }
}
