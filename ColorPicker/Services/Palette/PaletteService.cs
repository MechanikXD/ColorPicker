using System.Collections.ObjectModel;
using ColorPicker.Models.Colors;

namespace ColorPicker.Services.Palette;

public class PaletteService : IPaletteService
{
    public ColorPalette? CurrentPalette { get; private set; }
    public int CurrentPaletteIndex => AllPalettes.IndexOf(CurrentPalette!);
    public ObservableCollection<ColorPalette> AllPalettes { get; } = [];

    public event Action? CurrentPaletteChanged;
    public event Action? PalettesChanged;
    
    public void SelectPalette(ColorPalette palette)
    {
        var paletteIndex = AllPalettes.IndexOf(palette);
        if (paletteIndex < 0) return;
        
        CurrentPalette = palette;
        CurrentPaletteChanged?.Invoke();
        PalettesChanged?.Invoke();
    }

    public void AddPalette(ColorPalette palette)
    {
        AllPalettes.Add(palette);
        SelectPalette(palette);
    }

    public void RenamePalette(ColorPalette palette, string newName)
    {
        palette.Title = newName;
        PalettesChanged?.Invoke();
    }

    public void RenameCurrentPalette(string newName)
    {
        CurrentPalette?.Title = newName;
        PalettesChanged?.Invoke();
    }

    public void RemovePalette(ColorPalette palette)
    {
        AllPalettes.Remove(palette);
        if (CurrentPalette != palette) return;

        CurrentPalette = AllPalettes.Count > 0 ? AllPalettes[0] : null;
        CurrentPaletteChanged?.Invoke();
        PalettesChanged?.Invoke();
    }

    public void AddColor(ColorSwatch color)
    {
        CurrentPalette?.Palette.Add(color);
        PalettesChanged?.Invoke();
    }

    public void UpdateColor(ColorSwatch original, ColorSwatch updated, string? newName=null)
    {
        if (CurrentPalette == null) return;

        var colorIndex = CurrentPalette.Palette.IndexOf(original);
        if (colorIndex >= 0)
        {
            var old = CurrentPalette.Palette[colorIndex];
            CurrentPalette.Palette[colorIndex] = updated;
            CurrentPalette.Palette[colorIndex].Name = newName ?? old.Name;
        }
        PalettesChanged?.Invoke();
    }

    public void RemoveColor(ColorSwatch color)
    {
        CurrentPalette?.Palette.Remove(color);
        PalettesChanged?.Invoke();
    }
}