namespace ColorPicker.Models.Settings.Nodes;

public class GroupSetting : SettingNode
{
    public string Subtitle { get; init; } = "";
    
    public required IReadOnlyList<SettingNode> Children { get; init; }
}