using ColorPicker.Models.Settings.Nodes;

namespace ColorPicker.Models.Settings;

public static class SettingsLayout
{
    public static IReadOnlyList<SettingNode> GetLayout()
    {
        return [
            new GroupSetting {Title = "setting_color_settings_group", Children = GetColorRelatedSettings() },
            new GroupSetting {Title = "setting_history_settings_group", Children = GetHistoryRelatedSettings() },
            new TitleSetting {Title = "setting_system_title"},
            SettingsModels.ApplicationLanguage,
            SettingsModels.ApplicationTheme
        ];
    }

    private static IReadOnlyList<SettingNode> GetColorRelatedSettings()
    {
        return
        [
            SettingsModels.ColorSettings.MaxCombinationCount,
            SettingsModels.ColorSettings.CombinationRatioStep
        ];
    }

    private static IReadOnlyList<SettingNode> GetHistoryRelatedSettings()
    {
        SettingsModels.HistorySettings.RestrictHistoryLenght.OnSettingChanged += enabled =>
        {
            SettingsModels.HistorySettings.MaxHistoryLenght.IsEnabled = !(bool)enabled;
        };
        
        return
        [
            SettingsModels.HistorySettings.RestrictHistoryLenght,
            SettingsModels.HistorySettings.MaxHistoryLenght
        ];
    }
}