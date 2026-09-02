using ColorPicker.Services.Localization;
using CommunityToolkit.Mvvm.Messaging;

namespace ColorPicker.Models.Suggestion;

public class SuggestionMessage
{
    public required SuggestionMessageKind Kind { get; init; }
    public required Action<SuggestionMessage> RefreshLocalization { get; init; }
    public string Title { get; set; } = string.Empty;
    public string Body { get; set; } = string.Empty;

    public SuggestionMessage()
    {
        WeakReferenceMessenger.Default.Register<LocalizationService.CultureChangedMessage>(this,
            (_, _) => RefreshSettingLocalization());
    }

    public void RefreshSettingLocalization() => RefreshLocalization(this);
}