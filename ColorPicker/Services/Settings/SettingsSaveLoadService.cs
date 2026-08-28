using System.Text.Json;
using ColorPicker.Models.Settings;
using ColorPicker.Models.Settings.Nodes;
using ColorPicker.Models.StaticData;
using ColorPicker.Services.SaveLoad;
using ColorPicker.Services.SaveLoad.Serializable;

namespace ColorPicker.Services.Settings;

public class SettingsSaveLoadService : ISaveLoadService
{
    public Dictionary<string, ActiveSetting> SettingNodes { get; } = [];
    
    public void ParseSettingGroup(IReadOnlyList<SettingNode> nodes, HashSet<GroupSetting> navigatedSet)
    {
        foreach (var node in nodes)
        {
            if (node is GroupSetting group && navigatedSet.Add(group)) ParseSettingGroup(group.Children, navigatedSet);
            else if (node is ActiveSetting setting)
            {
                SettingNodes.Add(setting.Title, setting);
            }
        }
    }
    
    public void Load()
    {
        var json = Preferences.Get(UserStorageKeys.USER_SETTINGS_STORAGE_KEY, null);
        if (json == null)
        {
            LoadDefault();
            return;
        }

        var data = JsonSerializer.Deserialize(json, AppJsonContext.Default.SerializableSettings);

        if (data == null)
        {
            Clear();
            return;
        }

        foreach (var kvp in SettingNodes)
        {
            if (data.TryGetValue(kvp.Key, out var value) && value != null) kvp.Value.SetValue(value);
            else kvp.Value.SetDefaultValue();
            
            kvp.Value.OnSettingChanged += _ => Save();
        }
    }

    public void Save()
    {
        var serializable = SerializableSettings.ParseSettings(SettingNodes);
        var json = JsonSerializer.Serialize(serializable, AppJsonContext.Default.SerializableSettings);
        Preferences.Set(UserStorageKeys.USER_SETTINGS_STORAGE_KEY, json);
    }

    public void LoadDefault()
    {
        foreach (var node in SettingNodes.Values) node.SetDefaultValue();
    }

    public void Clear(bool loadDefault = true)
    {
        Preferences.Clear(UserStorageKeys.USER_SETTINGS_STORAGE_KEY);
        if (loadDefault) LoadDefault();
    }
}