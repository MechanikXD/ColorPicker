using System.Globalization;
using ColorPicker.Models.Settings;
using ColorPicker.Resources.Strings;

namespace ColorPicker.Services.Localization;

public class LocalizationService : ILocalizationService
{
    public string CurrentCulture { get; private set; } = "";

    private readonly Dictionary<string, string> _cultureCodeProxy = new()
    {
        ["System"] = "",
        ["English"] = "en",
        ["Ukrainian"] = "uk"
    };

    public LocalizationService()
    {
        SettingsModels.ApplicationLanguage.OnSettingChanged += UpdateCurrentCulture;
    }

    private void UpdateCurrentCulture(object newIndex)
    {
        CurrentCulture = _cultureCodeProxy[SettingsModels.ApplicationLanguage.GetCurrentOption()];
        SetCulture(CurrentCulture);
    }
    
    public void SetCulture(string cultureCode)
    {
        var newCulture = new CultureInfo(cultureCode);
        CultureInfo.CurrentCulture = newCulture;
        CultureInfo.CurrentUICulture = newCulture;
        AppResources.Culture = newCulture;
    }
}