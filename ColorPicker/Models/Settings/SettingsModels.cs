using ColorPicker.Models.Settings.Nodes;
using ColorPicker.Resources.Strings;

namespace ColorPicker.Models.Settings;

public static class SettingsModels
{
    public static class ColorSettings
    {
        public static readonly DropDownSetting CombinationRatioStep = new()
        {
            Id = "setting_combination_ratio_step",
            RefreshLocalization = self => { self.Title = AppResources.setting_combination_ratio_step; },
            Options = ["0.05", "0.1", "0.2", "0.25"],
            DefaultOption = 1
        };
        
        public static readonly DropDownSetting MaxCombinationCount = new()
        {
            Id = "setting_max_combinations_count",
            RefreshLocalization = self => { self.Title = AppResources.setting_max_combinations_count; },
            Options = ["5", "10", "20", "50"],
            DefaultOption = 1
        };
    }
    
    public static class HistorySettings
    {
        public static readonly DropDownSetting MaxHistoryLenght = new()
        {
            Id = "setting_max_history_lenght",
            RefreshLocalization = self => { self.Title = AppResources.setting_max_history_lenght; },
            Options = ["50", "100", "200", "500"],
            DefaultOption = 1
        };
        
        public static readonly ToggleSetting RestrictHistoryLenght = new()
        {
            Id = "setting_restrict_history_lenght",
            RefreshLocalization = self => { self.Title = AppResources.setting_restrict_history_lenght; },
            DefaultValue = true
        };
    }
    
    public static readonly DropDownSetting ApplicationTheme = new()
    {
        Id = "setting_application_theme",
        RefreshLocalization = self =>
        {
            self.Title = AppResources.setting_application_theme;
            if (self is DropDownSetting dropDown)
                dropDown.Options =
                [
                    AppResources.setting_option_system, AppResources.setting_option_theme_light, 
                    AppResources.setting_option_theme_dark
                ];
        },
        DefaultOption = 0
    };
    
    public static readonly DropDownSetting ApplicationLanguage = new()
    {
        Id = "setting_application_language",
        RefreshLocalization = self =>
        {
            self.Title = AppResources.setting_application_language;
            if (self is DropDownSetting dropDown)
                dropDown.Options = [AppResources.setting_option_system, "English", "Українська"];
        },
        DefaultOption = 0
    };
}