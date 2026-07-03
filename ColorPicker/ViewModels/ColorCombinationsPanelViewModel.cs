using System.Collections.ObjectModel;
using System.Windows.Input;
using ColorPicker.Models.Colors;

namespace ColorPicker.ViewModels;

public class ColorCombinationsPanelViewModel : BaseViewModel
{
    private bool _isExpanded = false;
    private Color _sourceColor = Colors.Transparent;

    public bool IsExpanded
    {
        get => _isExpanded;
        set => SetField(ref _isExpanded, value);
    }

    /// <summary>
    /// The color whose combinations are displayed.
    /// Set this from the parent VM whenever the picked color changes.
    /// </summary>
    public Color SourceColor
    {
        get => _sourceColor;
        set => SetField(ref _sourceColor, value);
    }

    /// <summary>Flat list of chips grouped by combination type.</summary>
    public ObservableCollection<ColorCombinationChip> Chips { get; } = [];

    public ICommand ToggleExpandCommand { get; }
    public ICommand SelectChipCommand { get; }

    public ColorCombinationsPanelViewModel()
    {
        ToggleExpandCommand = new Command(_ => { IsExpanded = !IsExpanded; });
        SelectChipCommand = new Command<ColorCombinationChip>(_ => { /* raise event / set parent color */ });
    }
}
