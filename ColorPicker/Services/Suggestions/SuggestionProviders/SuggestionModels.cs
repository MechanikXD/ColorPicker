using ColorPicker.Models.Suggestion;
using ColorPicker.Resources.Strings;

namespace ColorPicker.Services.Suggestions.SuggestionProviders;

public static class SuggestionModels
{
    public static class PaletteSuggestions
    {
        public static readonly SuggestionMessage NoPalette = new()
        {
            Kind = SuggestionMessageKind.Warning,
            Title = AppResources.suggestions_no_palettes_title,
            Body = AppResources.suggestions_no_palettes_body
        };
        
        public static readonly SuggestionMessage FewPalette = new()
        {
            Kind = SuggestionMessageKind.Notification,
            Title = AppResources.suggestions_few_palettes_title,
            Body = AppResources.suggestions_few_palettes_body
        };
        
        public static readonly SuggestionMessage NoColors = new()
        {
            Kind = SuggestionMessageKind.Warning,
            Title = AppResources.suggestions_no_colors_title,
            Body = AppResources.suggestions_no_colors_body
        };
        
        public static readonly SuggestionMessage FewColors = new()
        {
            Kind = SuggestionMessageKind.Suggestion,
            Title = AppResources.suggestions_few_colors_title,
            Body = AppResources.suggestions_few_colors_body
        };
        
        public static readonly SuggestionMessage LowCombinationStep = new()
        {
            Kind = SuggestionMessageKind.Notification,
            Title = AppResources.suggestions_low_combination_step_title,
            Body = AppResources.suggestions_low_combination_step_body
        };
    }

    public static class HistorySuggestions
    {
        public static readonly SuggestionMessage TooLongHistory = new()
        {
            Kind = SuggestionMessageKind.Suggestion,
            Title = AppResources.suggestions_long_history_title,
            Body = AppResources.suggestions_long_history_body
        };
    }
    
    public static class SystemSuggestions
    {
        public static readonly SuggestionMessage NoCameraPermission = new()
        {
            Kind = SuggestionMessageKind.Suggestion,
            Title = AppResources.suggestions_no_camera_permission_title,
            Body = AppResources.suggestions_no_camera_permission_body
        };
    }
}