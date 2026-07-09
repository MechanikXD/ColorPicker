using ColorPicker.Models.Colors;

namespace ColorPicker.Services.Color;

public static class ColorCombinationService
{
    public static IList<ColorCombination> GetCombinations(Microsoft.Maui.Graphics.Color source, ColorPalette palette) => [new ()];
}