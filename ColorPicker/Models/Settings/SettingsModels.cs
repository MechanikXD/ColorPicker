using ColorPicker.Models.Settings.Nodes;

namespace ColorPicker.Models.Settings;

public static class SettingsModels
{
    public static class ColorSettings
    {
        public static readonly DropDownSetting CombinationRatioStep = new()
        {
            Title = "combination_ratio_step",
            Options = ["0.05", "0.1", "0.2", "0.25"],
            DefaultOption = 1
        };
        
        public static readonly DropDownSetting MaxCombinationCount = new()
        {
            Title = "max_combinations_count",
            Options = ["5", "10", "20", "50"],
            DefaultOption = 1
        };
    }
    
    public static class HistorySettings
    {
        public static readonly DropDownSetting MaxHistoryLenght = new()
        {
            Title = "max_history_lenght",
            Options = ["50", "100", "200", "500"],
            DefaultOption = 1
        };
        
        public static readonly ToggleSetting RestrictHistoryLenght = new()
        {
            Title = "restrict_history_lenght",
            DefaultValue = true
        };
    }
    
    public static readonly DropDownSetting ApplicationTheme = new()
    {
        Title = "application_theme",
        Options = ["System", "Dark", "Light"],
        DefaultOption = 0
    };
    
    public static readonly DropDownSetting ApplicationLanguage = new()
    {
        Title = "application_language",
        Options = ["System", "English", "Ukrainian"],
        DefaultOption = 0
    };
}