using ColorPicker.Models.Suggestion;

namespace ColorPicker.Services.Suggestions.SuggestionProviders;

public static class SuggestionModels
{
    public static class PaletteSuggestions
    {
        public static readonly SuggestionMessage NoPalette =
            SuggestionFactory.GetWarning("suggestions_no_palettes_title", "suggestions_no_palettes_body");
        
        public static readonly SuggestionMessage FewPalette =
            SuggestionFactory.GetNotification("suggestions_few_palettes_title", "suggestions_few_palettes_body");
        
        public static readonly SuggestionMessage NoColors =
            SuggestionFactory.GetWarning("suggestions_no_colors_title", "suggestions_no_colors_body");
        
        public static readonly SuggestionMessage FewColors =
            SuggestionFactory.GetSuggestion("suggestions_few_colors_title", "suggestions_few_colors_body");
        
        public static readonly SuggestionMessage LowCombinationStep =
            SuggestionFactory.GetNotification("suggestions_low_combination_step_title", "suggestions_low_combination_step_body");
    }

    public static class HistorySuggestions
    {
        public static readonly SuggestionMessage TooLongHistory =
            SuggestionFactory.GetSuggestion("suggestions_long_history_title", "suggestions_long_history_body");
    }
    
    public static class SystemSuggestions
    {
        public static readonly SuggestionMessage NoCameraPermission =
            SuggestionFactory.GetSuggestion("suggestions_no_camera_permission_title", "suggestions_no_camera_permission_body");
    }
}