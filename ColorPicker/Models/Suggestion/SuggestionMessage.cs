namespace ColorPicker.Models.Suggestion;

public class SuggestionMessage
{
    public SuggestionMessageKind Kind { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Body { get; set; } = string.Empty;
}