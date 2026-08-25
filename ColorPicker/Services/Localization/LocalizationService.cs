using System.Globalization;
using ColorPicker.Resources.Strings;

namespace ColorPicker.Services.Localization;

public static class LocalizationService
{
    public static void ChangeCurrentLanguage(string cultureCode)
    {
        var culture = new CultureInfo(cultureCode);
        CultureInfo.CurrentCulture = culture;
        CultureInfo.CurrentUICulture = culture;
        AppResources.Culture = culture;
    }
}