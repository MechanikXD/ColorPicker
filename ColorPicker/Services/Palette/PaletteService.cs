using System.Collections.ObjectModel;
using ColorPicker.Models.Colors;

namespace ColorPicker.Services.Palette;

public class PaletteService : IPaletteService
{
    private readonly ObservableCollection<ColorPalette> _palettes = [];

    public ColorPalette? CurrentPalette { get; private set; }
    public int CurrentPaletteIndex { get; private set; }
    public IReadOnlyList<ColorPalette> AllPalettes => _palettes;
    
    public event Action? CurrentPaletteChanged;
    
    public void SelectPalette(ColorPalette palette)
    {
        var paletteIndex = _palettes.IndexOf(palette);
        if (paletteIndex < 0) return;
        
        CurrentPalette = palette;
        CurrentPaletteIndex = paletteIndex;
        CurrentPaletteChanged?.Invoke();
    }

    public void AddPalette(ColorPalette palette)
    {
        _palettes.Add(palette);
        SelectPalette(palette);
    }

    public void RenamePalette(ColorPalette palette, string newName) => palette.Title = newName;
    public void RenameCurrentPalette(string newName) => CurrentPalette?.Title = newName;

    public void RemovePalette(ColorPalette palette)
    {
        _palettes.Remove(palette);
        if (CurrentPalette != palette) return;

        if (_palettes.Count > 0)
        {
            CurrentPalette = _palettes[0];
            CurrentPaletteIndex = 0;
        }
        else
        {
            CurrentPalette = null;
            CurrentPaletteIndex = -1;
        }
        CurrentPaletteChanged?.Invoke();
    }

    public void AddColor(ColorSwatch color) => CurrentPalette?.Palette.Add(color);

    public void UpdateColor(ColorSwatch original, ColorSwatch updated)
    {
        if (CurrentPalette == null) return;

        var colorIndex = CurrentPalette.Palette.IndexOf(original);
        if (colorIndex >= 0) CurrentPalette.Palette[colorIndex] = updated;
    }

    public void RemoveColor(ColorSwatch color) => CurrentPalette?.Palette.Remove(color);
}