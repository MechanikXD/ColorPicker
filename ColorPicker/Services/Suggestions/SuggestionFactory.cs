using ColorPicker.Models.Suggestion;
using ColorPicker.Resources.Strings;

namespace ColorPicker.Services.Suggestions;

public static class SuggestionFactory
{
    public static SuggestionMessage GetNotification(string titleKey, string bodyKey) => new()
    {
        Kind = SuggestionMessageKind.Notification,
        RefreshLocalization = self =>
        {
            self.Title = Localize(titleKey);
            self.Body = Localize(bodyKey);
        },
        Title = Localize(titleKey),
        Body = Localize(bodyKey)
    };
    
    public static SuggestionMessage GetSuggestion(string titleKey, string bodyKey) => new()
    {
        Kind = SuggestionMessageKind.Suggestion,
        RefreshLocalization = self =>
        {
            self.Title = Localize(titleKey);
            self.Body = Localize(bodyKey);
        },
        Title = Localize(titleKey),
        Body = Localize(bodyKey)
    };
    
    public static SuggestionMessage GetWarning(string titleKey, string bodyKey) => new()
    {
        Kind = SuggestionMessageKind.Warning,
        RefreshLocalization = self =>
        {
            self.Title = Localize(titleKey);
            self.Body = Localize(bodyKey);
        },
        Title = Localize(titleKey),
        Body = Localize(bodyKey)
    };

    private static string Localize(string key)
    {
        return AppResources.ResourceManager.GetString(key) ?? "NO_LOCALIZATION_FOUND";
    }
}