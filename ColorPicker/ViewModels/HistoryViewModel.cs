using System.Collections.ObjectModel;
using System.Windows.Input;
using ColorPicker.Models.History;
using ColorPicker.Resources.Strings;
using ColorPicker.Services.History;
using ColorPicker.Services.Localization;
using CommunityToolkit.Mvvm.Messaging;

namespace ColorPicker.ViewModels;

public class HistoryViewModel : BaseViewModel
{
    private readonly IHistoryService _historyService;

    public ObservableCollection<HistoryEntry> Entries => _historyService.Entries;
    public bool IsNotEmpty { get; private set => SetField(ref field, value); } = true;
    public string EntriesCountString { get; private set => SetField(ref field, value); } = "";

    public ICommand RemoveEntryCommand { get; }
    public ICommand ClearAllCommand   { get; }

    public HistoryViewModel(IHistoryService historyService)
    {
        _historyService = historyService;
        _historyService.Entries.CollectionChanged += (_, _) => UpdateEntryFields();
        RemoveEntryCommand = new Command<HistoryEntry>(RemoveEntry);
        ClearAllCommand = new Command(ClearHistory);
        
        WeakReferenceMessenger.Default.Register<LocalizationService.CultureChangedMessage>(this, (_, _) => RefreshLocalization());
        UpdateEntryFields();
    }

    private void UpdateEntryFields()
    {
        IsNotEmpty = Entries.Count > 0;
        EntriesCountString = $"{Entries.Count} {AppResources.history_entries_count_suffix}";
    }

    private void RemoveEntry(HistoryEntry? entry)
    {
        if (entry is not null) _historyService.RemoveEntry(entry);
    }

    private void ClearHistory() => _historyService.Clear();

    public string LocalizedTitle => AppResources.history_title;
    public string LocalizedClearAll => AppResources.history_clear_all;
    public string LocalizedEmpty => AppResources.history_empty_history;
    public string LocalizedEmptySubtext => AppResources.history_empty_history_subtext;
    
    private void RefreshLocalization()
    {
        OnPropertyChanged(nameof(LocalizedTitle));
        OnPropertyChanged(nameof(LocalizedClearAll));
        OnPropertyChanged(nameof(LocalizedEmpty));
        OnPropertyChanged(nameof(LocalizedEmptySubtext));
    }
}