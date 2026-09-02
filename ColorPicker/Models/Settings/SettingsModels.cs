using ColorPicker.Models.Settings.Nodes;
using ColorPicker.Resources.Strings;

namespace ColorPicker.Models.Settings;

public static class SettingsModels
{
    public static class ColorSettings
    {
        public static readonly DropDownSetting CombinationRatioStep = new()
        {
            Title = AppResources.setting_combination_ratio_step,
            Options = ["0.05", "0.1", "0.2", "0.25"],
            DefaultOption = 1
        };
        
        public static readonly DropDownSetting MaxCombinationCount = new()
        {
            Title = AppResources.setting_max_combinations_count,
            Options = ["5", "10", "20", "50"],
            DefaultOption = 1
        };
    }
    
    public static class HistorySettings
    {
        public static readonly DropDownSetting MaxHistoryLenght = new()
        {
            Title = AppResources.setting_max_history_lenght,
            Options = ["50", "100", "200", "500"],
            DefaultOption = 1
        };
        
        public static readonly ToggleSetting RestrictHistoryLenght = new()
        {
            Title = AppResources.setting_restrict_history_lenght,
            DefaultValue = true
        };
    }
    
    public static readonly DropDownSetting ApplicationTheme = new()
    {
        Title = AppResources.setting_application_theme,
        Options = [AppResources.setting_option_system, AppResources.setting_option_theme_dark, AppResources.setting_option_theme_light],
        DefaultOption = 0
    };
    
    public static readonly DropDownSetting ApplicationLanguage = new()
    {
        Title = AppResources.setting_application_language,
        Options = [AppResources.setting_option_system, "English", "Українська"],
        DefaultOption = 0
    };
}