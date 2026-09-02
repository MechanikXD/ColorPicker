namespace ColorPicker.Models.Settings;

public abstract class ActiveSetting : SettingNode
{
    public bool IsEnabled { get; set => SetField(ref field, value); } = true;

    public abstract void SetDefaultValue();
    public abstract void SetValue(object newValue);
    
    public Action<object>? OnSettingChanged { get; set; }
}