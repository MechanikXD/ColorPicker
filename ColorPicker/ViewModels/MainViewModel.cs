using System.Collections.ObjectModel;
using System.Windows.Input;
using ColorPicker.Models.StaticData;
using ColorPicker.Models.Suggestion;
using ColorPicker.Resources.Strings;
using ColorPicker.Services.Localization;
using ColorPicker.Services.Navigation;
using ColorPicker.Services.Suggestions;
using CommunityToolkit.Mvvm.Messaging;

namespace ColorPicker.ViewModels;

public class MainViewModel : BaseViewModel
{
    private ISuggestionService _suggestionService; 
        
    public Color RandomPreviewColor
    {
        get;
        set => SetField(ref field, value);
    } = GetRandomColor();

    public ObservableCollection<SuggestionMessage> Messages { get; } = [];

    public ICommand OpenManualColorCommand { get; }
    public ICommand DismissMessageCommand { get; }

    public MainViewModel(ISuggestionService suggestionService)
    {
        _suggestionService = suggestionService;
        OpenManualColorCommand = new Command(NavigateToManualColorSelection);
        DismissMessageCommand = new Command<SuggestionMessage>(DismissMessage);
        
        WeakReferenceMessenger.Default.Register<LocalizationService.CultureChangedMessage>(this, (_, _) => RefreshLocalization());
        LoadMessages();
    }

    private async void NavigateToManualColorSelection()
    {
        await ShellNavigationService.GoToSubPageAsync(Pages.Sub.ManualColorSelection, new Dictionary<string, object>
        {
            [QueryAttributes.IS_EDIT_MODE] = false,
            [QueryAttributes.COLOR_HEX] = RandomPreviewColor.ToHex().TrimStart('#')
        });
    }
    
    private async void LoadMessages()
    {
        Messages.Clear();
        foreach (var suggestion in await _suggestionService.GetSuggestions()) Messages.Add(suggestion);
    }

    private void DismissMessage(SuggestionMessage target) => Messages.Remove(target);

    private static Color GetRandomColor()
    {
        var rng = new Random();
        return new Color(rng.Next(0, 256), rng.Next(0, 256), rng.Next(0, 256));
    }

    public string LocalizedWelcome => AppResources.main_welcome_message;
    public string LocalizedPickAColor => AppResources.main_pick_a_color;
    public string LocalizedColorSelection => AppResources.main_manual_color_selection;
    public string LocalizedColorSelectionSubtext => AppResources.main_manual_color_selection_subtext;
    public string LocalizedSuggestions => AppResources.main_suggestions;
    public string LocalizedSuggestionsEmpty => AppResources.main_suggestions_empty;
    public string LocalizedSuggestionsEmptySubtext => AppResources.main_suggestions_empty_subtext;
    
    private void RefreshLocalization()
    {
        OnPropertyChanged(nameof(LocalizedWelcome));
        OnPropertyChanged(nameof(LocalizedPickAColor));
        OnPropertyChanged(nameof(LocalizedColorSelection));
        OnPropertyChanged(nameof(LocalizedColorSelectionSubtext));
        OnPropertyChanged(nameof(LocalizedSuggestions));
        OnPropertyChanged(nameof(LocalizedSuggestionsEmpty));
        OnPropertyChanged(nameof(LocalizedSuggestionsEmptySubtext));
        LoadMessages();
    }
}
