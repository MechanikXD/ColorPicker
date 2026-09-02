using System.Collections.ObjectModel;
using System.Windows.Input;
using ColorPicker.Models.Colors;
using ColorPicker.Models.History;
using ColorPicker.Resources.Strings;
using ColorPicker.Services.Color;
using ColorPicker.Services.History;
using ColorPicker.Services.Localization;
using ColorPicker.Services.Palette;
using CommunityToolkit.Mvvm.Messaging;

namespace ColorPicker.ViewModels;

public class ColorCombinationsPanelViewModel : BaseViewModel
{
    private readonly IPaletteService _paletteService;
    private readonly IHistoryService _historyService;
    
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

    public ColorCombinationsPanelViewModel(IPaletteService paletteService, IHistoryService historyService)
    {
        _paletteService = paletteService;
        _historyService = historyService;
        RefreshCommand = new Command(async void (_) => await LoadCombinations());
        ToggleExpandCommand = new Command(async void (_) => await ToggleIsExpanded());
        UpdateCombinationsCount();
        
        WeakReferenceMessenger.Default.Register<LocalizationService.CultureChangedMessage>(this, (_, _) => RefreshLocalization());
    }

    private void UpdateCombinationsCount()
    {
        if (_lastCombinationsCount == Combinations.Count) return;
        _lastCombinationsCount = Combinations.Count;
        CombinationsCountText = $"{Combinations.Count} {AppResources.c_combinations_count}";
    }

    private async Task LoadCombinations()
    {
        IsLoading = true;
        Combinations.Clear();
        if (_paletteService.CurrentPalette == null) return;

        var combinations = await ColorCombinationService.GetCombinationsAsync(TargetColor, _paletteService.CurrentPalette);
        foreach (var combination in combinations) Combinations.Add(combination);
        _combinationsLoadedForColor = TargetColor;
        _historyService.CreateNewEntry(TargetColor, HistoryEntrySource.Combination);
        IsLoading = false;
    }

    private async Task ToggleIsExpanded()
    {
        var opening = !IsExpanded;
        IsExpanded = opening;
        if (opening && !Equals(_combinationsLoadedForColor, TargetColor)) await LoadCombinations();
    }

    public string LocalizedTitle => AppResources.c_combinations_title;
    public string LocalizedRefresh => AppResources.c_combinations_refresh;
    public string LocalizedEmpty => AppResources.c_combinations_empty;
    
    private void RefreshLocalization()
    {
        OnPropertyChanged(nameof(LocalizedTitle));
        OnPropertyChanged(nameof(LocalizedRefresh));
        OnPropertyChanged(nameof(LocalizedEmpty));
    }
}
