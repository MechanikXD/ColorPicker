using ColorPicker.Models.Suggestion;

namespace ColorPicker.Services.Suggestions;

public class SuggestionService : ISuggestionService
{
    private readonly IEnumerable<ISuggestionProvider> _providers;

    public SuggestionService(IEnumerable<ISuggestionProvider> providers)
    {
        _providers = providers;
    }
    
    public async Task<IReadOnlyList<SuggestionMessage>> GetSuggestions()
    {
        var result = new List<SuggestionMessage>();
        foreach (var provider in _providers) result.AddRange(await provider.GetSuggestions());
        return result;
    }
}