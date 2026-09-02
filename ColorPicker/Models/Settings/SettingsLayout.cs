using ColorPicker.Models.Settings.Nodes;
using ColorPicker.Resources.Strings;

namespace ColorPicker.Models.Settings;

public static class SettingsLayout
{
    public static IReadOnlyList<SettingNode> GetLayout()
    {
        return [
            new GroupSetting
            {
                Id = "setting_color_settings_group",
                RefreshLocalization = self => self.Title = AppResources.setting_color_settings_group, Children = GetColorRelatedSettings()
            },
            new GroupSetting
            {
                Id = "setting_history_settings_group",
                RefreshLocalization = self => self.Title = AppResources.setting_history_settings_group, Children = GetHistoryRelatedSettings()
            },
            new TitleSetting
            {
                Id = "setting_system_title",
                RefreshLocalization = self => self.Title = AppResources.setting_system_title
            },
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