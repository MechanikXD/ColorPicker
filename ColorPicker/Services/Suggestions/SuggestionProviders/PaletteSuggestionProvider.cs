using ColorPicker.Models.Suggestion;
using ColorPicker.Services.Palette;

namespace ColorPicker.Services.Suggestions.SuggestionProviders;

public class PaletteSuggestionProvider : ISuggestionProvider
{
    private readonly IPaletteService _paletteService;

    public PaletteSuggestionProvider(IPaletteService paletteService)
    {
        _paletteService = paletteService;
    }
    
    public IReadOnlyList<SuggestionMessage> GetSuggestions()
    {
        return [];
    }
}