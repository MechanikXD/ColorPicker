namespace ColorPicker.Models.Colors;

public class ColorCombinationChip
{
    public string Label { get; set; } = string.Empty;
    public Color Color { get; set; } = Microsoft.Maui.Graphics.Colors.Transparent;
    public string Hex { get; set; } = string.Empty;
    public string GroupName { get; set; } = string.Empty;
}