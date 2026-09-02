using ColorPicker.Models.Suggestion;

namespace ColorPicker.Services.Suggestions;

public interface ISuggestionProvider
{
    public IReadOnlyList<SuggestionMessage> GetSuggestions();
}