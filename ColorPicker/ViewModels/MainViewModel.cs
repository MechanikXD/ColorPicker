using System.Collections.ObjectModel;
using System.Windows.Input;
using ColorPicker.Models.Suggestion;
using ColorPicker.Services.Navigation;
using ColorPicker.Services.Suggestions;

namespace ColorPicker.ViewModels;

public class MainViewModel : BaseViewModel
{
    private const string WELCOME_MESSAGE = "Welcome!";
    
    public Color RandomPreviewColor
    {
        get;
        set => SetField(ref field, value);
    } = GetRandomColor();

    public string WelcomeMessage
    {
        get;
        set => SetField(ref field, value);
    } = WELCOME_MESSAGE;

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
        OpenManualColorCommand = new Command(_ =>
            ShellNavigationService.DoToSubPage("manualcolor",
                new Dictionary<string, object>
                    { ["isEditMode"] = false, ["colorHex"] = RandomPreviewColor.ToHex().TrimStart('#') }));
        DismissMessageCommand = new Command<SuggestionMessage>(DismissMessage);
        LoadMessages();
    }

    private void LoadMessages() => 
        Messages.Add(SuggestionFactory.GetNotification("Testing stuff", "Hold on a moment"));

    private void DismissMessage(SuggestionMessage target) => Messages.Remove(target);

    private static Color GetRandomColor()
    {
        var rng = new Random();
        return new Color(rng.Next(0, 256), rng.Next(0, 256), rng.Next(0, 256));
    }
}
