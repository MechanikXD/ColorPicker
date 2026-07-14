using System.Collections.ObjectModel;
using System.Windows.Input;
using ColorPicker.Models.Colors;
using ColorPicker.Services.Color;

namespace ColorPicker.ViewModels;

public class ColorCombinationsPanelViewModel : BaseViewModel
{
    private bool _isExpanded = false;
    private bool _isLoading  = false;
 
    public bool IsExpanded
    {
        get => _isExpanded;
        set => SetField(ref _isExpanded, value);
    }
 
    public bool IsLoading
    {
        get => _isLoading;
        set => SetField(ref _isLoading, value);
    }

    public Color TargetColor { get; set; } = Colors.Transparent;
    public ObservableCollection<ColorCombination> Combinations { get; } = [];

    public ICommand ToggleExpandCommand { get; }
    public ICommand RefreshCommand { get; }
    public ICommand SelectCombinationCommand { get; }

    public ColorCombinationsPanelViewModel()
    {
        RefreshCommand = new Command(_ => { LoadCombinations(); });
        
        ToggleExpandCommand = new Command(_ =>
        {
            var opening = !_isExpanded;
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
        foreach (var combination in ColorCombinationService.GetCombinations(TargetColor, new ColorPalette()))
            Combinations.Add(combination);
        IsLoading = false;
    }
}
