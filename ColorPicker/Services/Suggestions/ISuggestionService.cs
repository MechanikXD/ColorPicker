using ColorPicker.Models.Suggestion;

namespace ColorPicker.Services.Suggestions;

public interface ISuggestionService
{
    public IReadOnlyList<SuggestionMessage> GetSuggestions();
}