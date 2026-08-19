using System.Collections.ObjectModel;
using System.Windows.Input;
using ColorPicker.Models.StaticData;
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

    public ObservableCollection<SuggestionMessage> Messages { get; } = [];

    public ICommand OpenManualColorCommand { get; }
    public ICommand DismissMessageCommand { get; }

    public MainViewModel()
    {
        OpenManualColorCommand = new Command(NavigateToManualColorSelection);
        DismissMessageCommand = new Command<SuggestionMessage>(DismissMessage);
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
    
    private void LoadMessages()
    {
        Messages.Clear();
        foreach (var suggestion in SuggestionService.GetSuggestions()) Messages.Add(suggestion);
    }

    private void DismissMessage(SuggestionMessage target) => Messages.Remove(target);

    private static Color GetRandomColor()
    {
        var rng = new Random();
        return new Color(rng.Next(0, 256), rng.Next(0, 256), rng.Next(0, 256));
    }
}
