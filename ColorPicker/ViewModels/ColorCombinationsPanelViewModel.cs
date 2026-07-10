using System.Collections.ObjectModel;
using System.Windows.Input;
using ColorPicker.Models.Colors;
using ColorPicker.Services.Color;

namespace ColorPicker.ViewModels;

public class ColorCombinationsPanelViewModel : BaseViewModel
{
    public bool IsExpanded
    {
        get;
        set => SetField(ref field, value);
    } = false;

    public Color TargetColor
    {
        get;
        set => SetField(ref field, value);
    } = Colors.Transparent;

    public ObservableCollection<ColorCombination> Combinations { get; } = [];

    public ICommand ToggleExpandCommand { get; }
    public ICommand SelectCombinationCommand { get; }

    public ColorCombinationsPanelViewModel()
    {
        ToggleExpandCommand = new Command(_ => { IsExpanded = !IsExpanded; });
        SelectCombinationCommand = new Command<ColorCombination>(_ => { /* raise event / set parent color */ });
    }

    private void LoadCombinations()
    {
        Combinations.Clear();
        // TODO: Pull active palette
        foreach (var combination in ColorCombinationService.GetCombinations(TargetColor, new ColorPalette()))
            Combinations.Add(combination);
    }
}
