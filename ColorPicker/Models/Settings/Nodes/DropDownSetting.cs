namespace ColorPicker.Models.Settings.Nodes;

public class DropDownSetting : ActiveSetting
{
    public string Subtitle { get; init; } = "";
    public required IReadOnlyList<string> Options { get; init; }
    
    public required int DefaultOption { get; init; }

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

    public string GetCurrentOption() => Options[CurrentIndex];
    
    public override void SetDefaultValue() => CurrentIndex = DefaultOption;

    public override void SetValue(object newValue)
    {
        if (newValue is int newIndex) CurrentIndex = newIndex;
    }
}