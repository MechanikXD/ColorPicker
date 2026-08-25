using System.Windows.Input;
using ColorPicker.Resources.Strings;

namespace ColorPicker.ViewModels;

public class PromptViewModel : BaseViewModel
{
    private const string LOCALIZATION_NOT_FOUND_TEXT = "LOCALIZATION_NOT_FOUND";
    
    public bool IsVisible
    {
        get;
        set => SetField(ref field, value);
    }

    public string Title
    {
        get;
        set => SetField(ref field, value);
    } = string.Empty;

    public string Message
    {
        get;
        set => SetField(ref field, value);
    } = string.Empty;

    public string InputHint
    {
        get;
        set => SetField(ref field, value);
    } = string.Empty;

    public string InputText
    {
        get;
        set => SetField(ref field, value);
    } = string.Empty;

    public string ConfirmText
    {
        get;
        set => SetField(ref field, value);
    } = "Confirm";

    public bool ShowMessage
    {
        get;
        set => SetField(ref field, value);
    } = true;

    public bool ShowInput
    {
        get;
        set => SetField(ref field, value);
    }

    public bool IsDestructive
    {
        get;
        set => SetField(ref field, value);
    }

    private Action? _onConfirm;

    public ICommand ConfirmCommand { get; }
    public ICommand CancelCommand { get; }

    public PromptViewModel()
    {
        ConfirmCommand = new Command(Confirm);
        CancelCommand = new Command(Dismiss);
    }

    private void Confirm()
    {
        _onConfirm?.Invoke();
        Dismiss();
    }

    private void Dismiss()
    {
        IsVisible = false;
        _onConfirm = null;
        InputText = string.Empty;
    }
    
    public void Show(
        string title,
        string message = "",
        string confirmText = "Confirm",
        string inputText = "",
        string inputHint = "",
        bool showMessage = true,
        bool showInput = false,
        bool isDestructive = false,
        Action? onConfirm = null,
        bool useTranslate = true)
    {
        if (useTranslate) ShowTranslated(title, message, inputText, inputHint, showMessage, showInput, isDestructive, onConfirm);
        else ShowText(title, message, confirmText, inputText, inputHint, showMessage, showInput, isDestructive, onConfirm);
    }
    
    private void ShowText(
        string title,
        string message = "",
        string confirmText = "Confirm",
        string inputText = "",
        string inputHint = "",
        bool showMessage = true,
        bool showInput = false,
        bool isDestructive = false,
        Action? onConfirm = null)
    {
        Title = title;
        Message = message;
        ConfirmText = confirmText;
        InputHint = inputHint;
        InputText = inputText;
        ShowMessage = showMessage;
        ShowInput = showInput;
        IsDestructive = isDestructive;
        _onConfirm = onConfirm;
        IsVisible = true;
    }
    
    private void ShowTranslated(
        string title,
        string message = "",
        string inputText = "",
        string inputHint = "",
        bool showMessage = true,
        bool showInput = false,
        bool isDestructive = false,
        Action? onConfirm = null)
    {
        var rm = AppResources.ResourceManager;
        Title = rm.GetString(title) ?? LOCALIZATION_NOT_FOUND_TEXT;
        if (showMessage) Message = rm.GetString(message) ?? LOCALIZATION_NOT_FOUND_TEXT;
        ConfirmText = AppResources.button_confirm ?? LOCALIZATION_NOT_FOUND_TEXT;
        if (showInput) InputHint = rm.GetString(inputHint) ?? LOCALIZATION_NOT_FOUND_TEXT;
        if (showInput) InputText = rm.GetString(inputText) ?? LOCALIZATION_NOT_FOUND_TEXT;
        ShowMessage = showMessage;
        ShowInput = showInput;
        IsDestructive = isDestructive;
        _onConfirm = onConfirm;
        IsVisible = true;
    }
}