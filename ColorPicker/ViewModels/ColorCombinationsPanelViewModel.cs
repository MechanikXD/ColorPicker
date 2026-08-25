using System.Collections.ObjectModel;
using System.Windows.Input;
using ColorPicker.Models.Colors;
using ColorPicker.Resources.Strings;
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

    public string CombinationsCountText
    {
        get;
        set => SetField(ref field, value);
    } = "";
 
    public bool IsLoading
    {
        get;
        set => SetField(ref field, value);
    }

    public Color TargetColor { get; set; } = Colors.Transparent;
    private Color _combinationsLoadedForColor = Colors.Transparent;
    private int _lastCombinationsCount = -1;
    public ObservableCollection<ColorCombination> Combinations { get; } = [];

    public ICommand ToggleExpandCommand { get; }
    public ICommand RefreshCommand { get; }

    public ColorCombinationsPanelViewModel(IPaletteService paletteService)
    {
        _paletteService = paletteService;
        RefreshCommand = new Command(async void (_) => await LoadCombinations());
        ToggleExpandCommand = new Command(async void (_) => await ToggleIsExpanded());
        UpdateCombinationsCount();
    }

    private void UpdateCombinationsCount()
    {
        if (_lastCombinationsCount == Combinations.Count) return;
        _lastCombinationsCount = Combinations.Count;
        CombinationsCountText = $"{_lastCombinationsCount} {AppResources.c_combinations_count}";
    }

    private async Task LoadCombinations()
    {
        IsLoading = true;
        Combinations.Clear();
        if (_paletteService.CurrentPalette == null) return;

        var combinations = await ColorCombinationService.GetCombinationsAsync(TargetColor, _paletteService.CurrentPalette);
        foreach (var combination in combinations) Combinations.Add(combination);
        _combinationsLoadedForColor = TargetColor;
        IsLoading = false;
    }

    private async Task ToggleIsExpanded()
    {
        var opening = !IsExpanded;
        IsExpanded = opening;
        if (opening && !Equals(_combinationsLoadedForColor, TargetColor)) await LoadCombinations();
    }
}
