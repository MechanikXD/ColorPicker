using ColorPicker.Models.Colors;

namespace ColorPicker.Services.Palette;

public interface IPaletteService
{
    public ColorPalette? CurrentPalette { get; }
    public int CurrentPaletteIndex { get; }
    public IReadOnlyList<ColorPalette> AllPalettes { get; }

    public void AddPalette(ColorPalette palette);
    public void SelectPalette(ColorPalette palette);
    public void RenamePalette(ColorPalette palette, string newName);
    public void RemovePalette(ColorPalette palette);
    
    public void AddColor(ColorSwatch color);
    public void UpdateColor(ColorSwatch original, ColorSwatch updated);
    public void RemoveColor(ColorSwatch color);

    public event Action? CurrentPaletteChanged;
}