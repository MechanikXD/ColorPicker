using System.Text.Json;
using ColorPicker.Models.StaticData;
using ColorPicker.Services.SaveLoad;
using ColorPicker.Services.SaveLoad.Serializable;

namespace ColorPicker.Services.History;

public class HistorySaveLoadService : ISaveLoadService
{
    private readonly IHistoryService _historyService;

    public HistorySaveLoadService(IHistoryService historyService)
    {
        _historyService = historyService;
    }
    
    public void Load()
    {
        _historyService.Entries.CollectionChanged += (_, _) => Save();
        var json = Preferences.Get(UserStorageKeys.USER_HISTORY_STORAGE_KEY, null);
        if (json == null)
        {
            LoadDefault();
            return;
        }

        var data = JsonSerializer.Deserialize(json, AppJsonContext.Default.ListSerializableHistoryEntry) ?? []; 
        
        _historyService.Clear();
        foreach (var entry in data) 
            _historyService.PushBackEntry(entry.ToEntry());
    }
    
    public void Save()
    {
        var serializable = _historyService.Entries.Select(SerializableHistoryEntry.FromEntry).ToList();
        var json = JsonSerializer.Serialize(serializable, AppJsonContext.Default.ListSerializableHistoryEntry);
        Preferences.Set(UserStorageKeys.USER_HISTORY_STORAGE_KEY, json);
    }

    public void LoadDefault() => _historyService.Clear();

    public void Clear(bool loadDefault = true)
    {
        Preferences.Remove(UserStorageKeys.USER_HISTORY_STORAGE_KEY);
        if (loadDefault) LoadDefault();
    }
}