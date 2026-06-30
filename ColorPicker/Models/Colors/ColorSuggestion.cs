namespace ColorPicker.Models.Colors;

public class ColorSuggestion
{
    public ColorSwatch SuggestedColor { get; set; } = new();
    public SuggestionKind SuggestionKind { get; set; }
    public string SuggestionReason { get; set; } = string.Empty;
}