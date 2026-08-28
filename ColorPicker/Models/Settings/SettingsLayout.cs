using ColorPicker.Models.Settings.Nodes;

namespace ColorPicker.Models.Settings;

public static class SettingsLayout
{
    public static IReadOnlyList<SettingNode> GetLayout()
    {
        return [
            new TitleSetting {Title = "Settings title"},
            new DropDownSetting {Options = ["Hello", "World!"], Title = "Greetings"},
            new GroupSetting {Title = "Tap me", Children = [
                new ToggleSetting {Title = "Switcher"}
            ]}
        ];
    }
}