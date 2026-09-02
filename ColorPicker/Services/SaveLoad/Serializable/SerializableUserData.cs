using ColorPicker.Services.Palette;

namespace ColorPicker.Services.SaveLoad.Serializable;

public class SerializableUserData
{
    public int ActivePaletteIndex { get; set; } = -1;
    public List<SerializablePalette> Palette { get; set; } = [];

    public static SerializableUserData FromService(IPaletteService paletteService)
    {
        return new SerializableUserData
        {
            ActivePaletteIndex = paletteService.CurrentPaletteIndex,
            Palette = paletteService.AllPalettes.Select(SerializablePalette.FromPalette).ToList()
        };
    }
}