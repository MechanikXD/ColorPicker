using System.Globalization;
using ColorPicker.Models.Settings;
using ColorPicker.Resources.Strings;
using CommunityToolkit.Mvvm.Messaging;

namespace ColorPicker.Services.Localization;

public static class LocalizationService
{
    public static string CurrentCulture { get; private set; } = "";

    private static readonly Dictionary<string, string> CultureCodeProxy = new()
    {
        ["System"] = "",
        ["English"] = "en",
        ["Ukrainian"] = "uk"
    };

    public static void Initialize()
    {
        SettingsModels.ApplicationLanguage.OnSettingChanged += UpdateCurrentCulture;
    }

    private static void UpdateCurrentCulture(object newIndex)
    {
        CurrentCulture = CultureCodeProxy[SettingsModels.ApplicationLanguage.GetCurrentOption()];
        SetCulture(CurrentCulture);
    }
    
    public static void SetCulture(string cultureCode)
    {
        var newCulture = new CultureInfo(cultureCode);
        CultureInfo.CurrentCulture = newCulture;
        CultureInfo.CurrentUICulture = newCulture;
        CultureInfo.DefaultThreadCurrentCulture = newCulture;
        CultureInfo.DefaultThreadCurrentUICulture = newCulture;
        AppResources.Culture = newCulture;
        
        MainThread.BeginInvokeOnMainThread(() => WeakReferenceMessenger.Default.Send(new CultureChangedMessage()));
    }

    public class CultureChangedMessage;
}