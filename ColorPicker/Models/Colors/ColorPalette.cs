using System.Collections.ObjectModel;
using ColorPicker.Resources.Strings;

namespace ColorPicker.Models.Colors;

public class ColorPalette
{
    public string Title { get; set; } = AppResources.palettes_default_title;
    public ObservableCollection<ColorSwatch> Palette { get; set; } = [];
}