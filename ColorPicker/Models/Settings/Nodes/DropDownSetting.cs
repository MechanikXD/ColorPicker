namespace ColorPicker.Models.Settings.Nodes;

public class DropDownSetting : SettingNode
{
    public string Subtitle { get; init; } = "";
    public required IReadOnlyList<string> Options { get; init; }
    
    public int CurrentIndex
    {
        get;
        set
        {
            if (value == field) return;
            SetField(ref field, value);
            OnSettingChanged?.Invoke(value);
        }
    }
    
    public Action<int>? OnSettingChanged { get; set; }
}