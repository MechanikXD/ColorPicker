using ColorPicker.Models.Suggestion;

namespace ColorPicker.Services.Selectors;

public class SuggestionTemplateSelector : DataTemplateSelector
{
    public DataTemplate? NotificationTemplate { get; set; }
    public DataTemplate? SuggestionTemplate { get; set; }
    public DataTemplate? WarningTemplate { get; set; }

    protected override DataTemplate? OnSelectTemplate(object item, BindableObject container) =>
        item is SuggestionMessage msg
            ? msg.Kind switch
            {
                SuggestionMessageKind.Warning => WarningTemplate,
                SuggestionMessageKind.Suggestion => SuggestionTemplate,
                _ => NotificationTemplate
            }
            : NotificationTemplate;
}
