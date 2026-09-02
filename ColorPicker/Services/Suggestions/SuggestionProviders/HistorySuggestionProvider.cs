using ColorPicker.Models.Suggestion;
using ColorPicker.Services.History;

namespace ColorPicker.Services.Suggestions.SuggestionProviders;

public class HistorySuggestionProvider : ISuggestionProvider
{
    private const int HISTORY_ENTRY_THRESHOLD = 100;
    private readonly IHistoryService _historyService;

    public HistorySuggestionProvider(IHistoryService historyService)
    {
        _historyService = historyService;
    }
    
    public async Task<IReadOnlyList<SuggestionMessage>> GetSuggestions()
    {
        return _historyService.Entries.Count > HISTORY_ENTRY_THRESHOLD
            ? [SuggestionModels.HistorySuggestions.TooLongHistory]
            : [];
    }
}