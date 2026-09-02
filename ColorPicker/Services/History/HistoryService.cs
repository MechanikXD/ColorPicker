using System.Collections.ObjectModel;
using ColorPicker.Models.History;
using ColorPicker.Models.Settings;

namespace ColorPicker.Services.History;

public class HistoryService : IHistoryService
{
    private bool _restrictHistoryLength;
    private int _maxHistoryLenght;
    public ObservableCollection<HistoryEntry> Entries { get; } = [];
    
    public HistoryService()
    {
        SettingsModels.HistorySettings.MaxHistoryLenght.OnSettingChanged += UpdateMaxHistoryLenght;
        SettingsModels.HistorySettings.RestrictHistoryLenght.OnSettingChanged += UpdateRestrictHistoryLength;
    }

    private void UpdateMaxHistoryLenght(object newIndex) => 
        _maxHistoryLenght = int.Parse(SettingsModels.HistorySettings.MaxHistoryLenght.GetCurrentOption());
    
    private void UpdateRestrictHistoryLength(object newValue) => 
        _restrictHistoryLength = (bool)newValue;

    public void CreateNewEntry(Microsoft.Maui.Graphics.Color color, HistoryEntrySource source)
    {
        var entry = new HistoryEntry(color, source, DateTimeOffset.UtcNow);
        Entries.Insert(0, entry);
        
        if (_restrictHistoryLength && Entries.Count > _maxHistoryLenght) Entries.Remove(Entries[^1]);
    }

    public void PushBackEntry(HistoryEntry entry)
    {
        Entries.Add(entry);
        if (_restrictHistoryLength && Entries.Count > _maxHistoryLenght) Entries.Remove(Entries[^1]);
    }

    public void RemoveEntry(HistoryEntry entry) => Entries.Remove(entry);

    public void Clear() => Entries.Clear();
}