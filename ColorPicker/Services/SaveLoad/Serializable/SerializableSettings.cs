using ColorPicker.Models.Settings;
using ColorPicker.Models.Settings.Nodes;

namespace ColorPicker.Services.SaveLoad.Serializable;

public class SerializableSettings
{
    public Dictionary<string, int> DropDownSettings { get; set; } = [];
    public Dictionary<string, bool> ToggleSettings { get; set; } = [];

    public bool TryGetValue(string key, out object? value)
    {
        value = null;
        if (DropDownSettings.TryGetValue(key, out var i))
        {
            value = i;
            return true;
        };
        if (ToggleSettings.TryGetValue(key, out var b))
        {
            value = b;
            return true;
        }
        return false;
    }
    
    public static SerializableSettings ParseSettings(Dictionary<string, ActiveSetting> settings)
    {
        var toggles = new Dictionary<string, bool>();
        var dropdowns = new Dictionary<string, int>();
        
        foreach (var setting in settings.Values)
        {
            switch (setting)
            {
                case ToggleSetting toggle when toggle.DefaultValue != toggle.Value:
                    toggles.Add(toggle.Id, toggle.Value);
                    break;
                case DropDownSetting dropDown when dropDown.DefaultOption != dropDown.CurrentIndex:
                    dropdowns.Add(dropDown.Id, dropDown.CurrentIndex);
                    break;
            }
        }

        return new SerializableSettings { DropDownSettings = dropdowns, ToggleSettings = toggles };
    }
}