using System.Collections.ObjectModel;
using ColorPicker.Models.History;

namespace ColorPicker.Services.History;

public class HistoryService : IHistoryService
{
    public ObservableCollection<HistoryEntry> Entries { get; } = [];
    
    public void CreateNewEntry(Microsoft.Maui.Graphics.Color color, HistoryEntrySource source)
    {
        var entry = new HistoryEntry(color, source, DateTimeOffset.UtcNow);
        Entries.Insert(0, entry);
    }

    public void AddEntry(Microsoft.Maui.Graphics.Color color, HistoryEntrySource source, DateTimeOffset date, int index=-1)
    {
        var entry = new HistoryEntry(color, source, date);
        if (index == -1) Entries.Add(entry);
        else Entries.Insert(index, entry);
    }

    public void RemoveEntry(HistoryEntry entry) => Entries.Remove(entry);

    public void Clear() => Entries.Clear();
}