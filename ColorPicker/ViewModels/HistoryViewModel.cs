using System.Collections.ObjectModel;
using System.Windows.Input;
using ColorPicker.Models.History;
using ColorPicker.Resources.Strings;
using ColorPicker.Services.History;

namespace ColorPicker.ViewModels;

public class HistoryViewModel : BaseViewModel
{
    private readonly IHistoryService _historyService;

    public ObservableCollection<HistoryEntry> Entries => _historyService.Entries;
    public bool IsEmpty { get; private set; } = true;
    public string EntriesCountString { get; private set; } = string.Empty;

    public ICommand RemoveEntryCommand { get; }
    public ICommand ClearAllCommand   { get; }

    public HistoryViewModel(IHistoryService historyService)
    {
        _historyService = historyService;
        _historyService.Entries.CollectionChanged += (_, _) => UpdateEntryFields();
        RemoveEntryCommand = new Command<HistoryEntry>(RemoveEntry);
        ClearAllCommand = new Command(ClearHistory);
        UpdateEntryFields();
    }

    private void UpdateEntryFields()
    {
        IsEmpty = Entries.Count == 0;
        EntriesCountString = $"{Entries.Count} {AppResources.history_entries_count_suffix}";
    }

    private void RemoveEntry(HistoryEntry? entry)
    {
        if (entry is not null) _historyService.RemoveEntry(entry);
    }

    private void ClearHistory() => _historyService.Clear();
}