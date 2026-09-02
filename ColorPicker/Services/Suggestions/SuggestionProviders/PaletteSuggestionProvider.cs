using ColorPicker.Models.Suggestion;
using ColorPicker.Services.Palette;

namespace ColorPicker.Services.Suggestions.SuggestionProviders;

public class PaletteSuggestionProvider : ISuggestionProvider
{
    private const int FEW_PALETTES_THRESHOLD = 1;
    private const int FEW_COLORS_THRESHOLD = 5; 
    
    private readonly IPaletteService _paletteService;

    public PaletteSuggestionProvider(IPaletteService paletteService)
    {
        _paletteService = paletteService;
    }
    
    public async Task<IReadOnlyList<SuggestionMessage>> GetSuggestions()
    {
        if (_paletteService.AllPalettes.Count == 0)
            return [SuggestionModels.PaletteSuggestions.NoPalette];

        HashSet<SuggestionMessage> result = [];
        if (_paletteService.AllPalettes.Count <= FEW_PALETTES_THRESHOLD) 
            result.Add(SuggestionModels.PaletteSuggestions.FewPalette);

        foreach (var palette in _paletteService.AllPalettes)
        {
            if (palette.Palette.Count == 0) 
                result.Add(SuggestionModels.PaletteSuggestions.NoColors);
            else if (palette.Palette.Count <= FEW_COLORS_THRESHOLD) 
                result.Add(SuggestionModels.PaletteSuggestions.FewColors);
        }
        
        return result.ToList();
    }
}

