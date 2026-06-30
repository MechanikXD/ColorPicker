using System.Collections.ObjectModel;

namespace ColorPicker.Models.Colors;

public class ColorPalette
{
    public string Title { get; set; } = "Color Palette";
    public ObservableCollection<ColorSwatch> Palette { get; set; } = [];
}