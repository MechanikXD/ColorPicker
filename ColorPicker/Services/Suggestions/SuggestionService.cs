using ColorPicker.Models.Suggestion;

namespace ColorPicker.Services.Suggestions;

public static class SuggestionService
{
    public static SuggestionMessage[] GetSuggestions()
    {
        return [SuggestionFactory.GetNotification("Testing stuff", "Hold on a moment")];
    }
}