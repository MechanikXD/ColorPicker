using ColorPicker.Models.Suggestion;

namespace ColorPicker.Services.Suggestions;

public static class SuggestionFactory
{
    public static SuggestionMessage GetNotification(string title, string body) => new()
    {
        Kind = SuggestionMessageKind.Notification,
        Title = title,
        Body = body
    };
    
    public static SuggestionMessage GetSuggestion(string title, string body) => new()
    {
        Kind = SuggestionMessageKind.Suggestion,
        Title = title,
        Body = body
    };
    
    public static SuggestionMessage GetWarning(string title, string body) => new()
    {
        Kind = SuggestionMessageKind.Warning,
        Title = title,
        Body = body
    };
}