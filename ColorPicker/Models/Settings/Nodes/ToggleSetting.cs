namespace ColorPicker.Models.Settings.Nodes;

public class ToggleSetting : SettingNode
{
    public string Subtitle { get; init; } = "";
    
    public bool Value
    {
        get;
        set
        {
            if (value == field) return;
            SetField(ref field, value);
            OnSettingChanged?.Invoke(value);
        }
    }

    public Action<bool>? OnSettingChanged { get; set; }
}