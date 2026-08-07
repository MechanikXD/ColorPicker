using ColorPicker.Models.Colors;

namespace ColorPicker.Services.SaveLoad.Serializable;

public class SerializableColor
{
    public string Name { get; set; } = string.Empty;
    public string Hex { get; set; } = string.Empty;

    public ColorSwatch ToSwatch() => ColorSwatch.FromColor(Microsoft.Maui.Graphics.Color.FromArgb(Hex), Name);

    public static SerializableColor FromSwatch(ColorSwatch swatch) =>
        new()
        {
            Name = swatch.Name,
            Hex = swatch.Hex
        };
}