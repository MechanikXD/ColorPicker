using ColorPicker.Models.Colors;
using CommunityToolkit.Maui.Core.Extensions;

namespace ColorPicker.Services.SaveLoad.Serializable;

public record SerializablePalette
{
    public string Name { get; set; } = string.Empty;
    public List<SerializableColor> Colors { get; set; } = [];

    public ColorPalette ToPalette() =>
        new()
        {
            Title = Name,
            Palette = Colors.Select(c => c.ToSwatch()).ToObservableCollection()
        };

    public static SerializablePalette FromPalette(ColorPalette palette) =>
        new()
        {
            Name = palette.Title,
            Colors = palette.Palette.Select(SerializableColor.FromSwatch).ToList()
        };
}