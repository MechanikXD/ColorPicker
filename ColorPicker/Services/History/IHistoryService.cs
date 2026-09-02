using System.Collections.ObjectModel;
using ColorPicker.Models.History;

namespace ColorPicker.Services.History;

public interface IHistoryService
{
    ObservableCollection<HistoryEntry> Entries { get; }
 
    void CreateNewEntry(Microsoft.Maui.Graphics.Color color, HistoryEntrySource source);
    void PushBackEntry(HistoryEntry entry);
    void RemoveEntry(HistoryEntry entry);
    void Clear();
}