using System.Collections.ObjectModel;
using System.Windows.Input;
using ColorPicker.Models.Suggestion;

namespace ColorPicker.ViewModels;

public class MainViewModel : BaseViewModel
{
    private Color _randomPreviewColor = Colors.Transparent;
    private string _welcomeMessage = string.Empty;

    public Color RandomPreviewColor
    {
        get => _randomPreviewColor;
        set => SetField(ref _randomPreviewColor, value);
    }

    public string WelcomeMessage
    {
        get => _welcomeMessage;
        set => SetField(ref _welcomeMessage, value);
    }

    /// <summary>
    /// Mixed list of SuggestionMessage items. The CollectionView uses a
    /// DataTemplateSelector to pick the right card style per Kind.
    /// When empty of Suggestion/Warning items, a Notification placeholder is shown.
    /// </summary>
    public ObservableCollection<SuggestionMessage> Messages { get; } = new();

    /// <summary>Navigates to ManualColorPage with RandomPreviewColor pre-loaded.</summary>
    public ICommand OpenManualColorCommand { get; }

    /// <summary>Dismiss a suggestion or warning card.</summary>
    public ICommand DismissMessageCommand { get; }

    public MainViewModel()
    {
        OpenManualColorCommand = new Command(_ => { /* Shell.Current.GoToAsync("manualcolor") */ });
        DismissMessageCommand = new Command<SuggestionMessage>(_ => { });
    }
}
