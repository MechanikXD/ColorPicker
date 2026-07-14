using System.Collections.ObjectModel;
using System.Windows.Input;
using ColorPicker.Models.Colors;
using ColorPicker.Services.Color;
using ColorPicker.Services.Palette;

namespace ColorPicker.ViewModels;

public class ColorCombinationsPanelViewModel : BaseViewModel
{
    private readonly IPaletteService _paletteService;
    
    public bool IsExpanded
    {
        get;
        set => SetField(ref field, value);
    }
 
    public bool IsLoading
    {
        get;
        set => SetField(ref field, value);
    }

    public Color TargetColor { get; set; } = Colors.Transparent;
    public ObservableCollection<ColorCombination> Combinations { get; } = [];

    public ICommand ToggleExpandCommand { get; }
    public ICommand RefreshCommand { get; }
    public ICommand SelectCombinationCommand { get; }

    public ColorCombinationsPanelViewModel(IPaletteService paletteService)
    {
        _paletteService = paletteService;
        RefreshCommand = new Command(_ => { LoadCombinations(); });
        
        ToggleExpandCommand = new Command(_ =>
        {
            var opening = !IsExpanded;
            IsExpanded = opening;
            // Auto-compute on first open so there's something to show.
            // Subsequent opens show cached results until Refresh is pressed.
            if (opening && Combinations.Count == 0)
                RefreshCommand.Execute(null);
        });
 
        SelectCombinationCommand = new Command<ColorCombination>(_ =>
        {
            // Raise event or callback to parent VM
        });
    }

    private void LoadCombinations()
    {
        Combinations.Clear();
        if (_paletteService.CurrentPalette == null) return;
        
        foreach (var combination in ColorCombinationService.GetCombinations(TargetColor, _paletteService.CurrentPalette))
            Combinations.Add(combination);
        IsLoading = false;
    }
}
