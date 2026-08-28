namespace ColorPicker.Models.Settings.Nodes;

public class ToggleSetting : ActiveSetting
{
    public string Subtitle { get; init; } = "";
    
    public required bool DefaultValue { get; init; }
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

    public override void SetDefaultValue() => Value = DefaultValue;

    public override void SetValue(object newValue)
    {
        if (newValue is bool newToggleValue) Value = newToggleValue;
    }
}