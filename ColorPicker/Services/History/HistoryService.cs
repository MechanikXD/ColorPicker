using System.Collections.ObjectModel;
using ColorPicker.Models.History;

namespace ColorPicker.Services.History;

public class HistoryService : IHistoryService
{
    private const int MAX_HISTORY_LENGHT = 100;
    public ObservableCollection<HistoryEntry> Entries { get; } = [];
    
    public void CreateNewEntry(Microsoft.Maui.Graphics.Color color, HistoryEntrySource source)
    {
        var entry = new HistoryEntry(color, source, DateTimeOffset.UtcNow);
        Entries.Insert(0, entry);
        
        if (MAX_HISTORY_LENGHT != -1 && Entries.Count > MAX_HISTORY_LENGHT) Entries.Remove(Entries[^1]);
    }

    public void PushBackEntry(HistoryEntry entry)
    {
        Entries.Add(entry);
        if (MAX_HISTORY_LENGHT != -1 && Entries.Count > MAX_HISTORY_LENGHT) Entries.Remove(Entries[^1]);
    }

    public void RemoveEntry(HistoryEntry entry) => Entries.Remove(entry);

    public void Clear() => Entries.Clear();
}