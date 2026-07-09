using System.Collections.ObjectModel;
using System.Windows.Input;
using ColorPicker.Models.Suggestion;
using ColorPicker.Services.Suggestions;

namespace ColorPicker.ViewModels;

public class MainViewModel : BaseViewModel
{
    public Color RandomPreviewColor
    {
        get;
        set => SetField(ref field, value);
    } = Colors.DarkRed;

    public string WelcomeMessage
    {
        get;
        set => SetField(ref field, value);
    } = "Hi!";

    /// Mixed list of SuggestionMessage items. The CollectionView uses a
    /// DataTemplateSelector to pick the right card style per Kind.
    /// When empty of Suggestion/Warning items, a Notification placeholder is shown.
    public ObservableCollection<SuggestionMessage> Messages { get; } = [];

    // Navigates to ManualColorPage with RandomPreviewColor preloaded
    public ICommand OpenManualColorCommand { get; }

    // Dismiss a suggestion or warning card.
    public ICommand DismissMessageCommand { get; }

    public MainViewModel()
    {
        OpenManualColorCommand = new Command(_ => Shell.Current.GoToAsync("manualcolor"));
        DismissMessageCommand = new Command<SuggestionMessage>(DismissMessage);
        LoadMessages();
    }

    private void LoadMessages()
    {
        Messages.Add(SuggestionFactory.GetNotification("Testing stuff", "Hold on a moment"));
    }

    private static void DismissMessage(SuggestionMessage target) { }
}
