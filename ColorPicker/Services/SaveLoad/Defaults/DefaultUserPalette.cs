using ColorPicker.Services.SaveLoad.Serializable;

namespace ColorPicker.Services.SaveLoad.Defaults;

public static class DefaultUserPalette
{
    public const string DEFAULT_PALETTE_TITLE = "Default";
    
    public static SerializableUserData UserData { get; } = new()
    {
        ActivePaletteIndex = 0,
        Palette =
        [
            new SerializablePalette
            {
                Name = DEFAULT_PALETTE_TITLE,
                Colors = [new SerializableColor
                {
                    Hex = "#000000",
                    Name = "Black"
                },
                    new SerializableColor
                {
                    Hex = "#FFFFFF",
                    Name = "White"
                },
                    new SerializableColor
                {
                    Hex = "#FF0000",
                    Name = "Red"
                },
                    new SerializableColor
                {
                    Hex = "#00FF00",
                    Name = "Green"
                },
                    new SerializableColor
                {
                    Hex = "#0000FF",
                    Name = "Blue"
                },
                    new SerializableColor
                {
                    Hex = "#FFFF00",
                    Name = "Yellow"
                },
                    new SerializableColor
                {
                    Hex = "#FF00FF",
                    Name = "Pink"
                },
                    new SerializableColor
                {
                    Hex = "#00FFFF",
                    Name = "Cyan"
                },
                    new SerializableColor
                {
                    Hex = "#FF8800",
                    Name = "Orange"
                },]
            }
        ]
    };
}