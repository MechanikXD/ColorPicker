using System.Globalization;
using ColorPicker.Models.StaticData;
using ColorPicker.Services.SaveLoad;

namespace ColorPicker.Services.Localization;

public class LocalizationSaveLoadService : ISaveLoadService
{
    public void Load()
    {
        var selectedCulture = Preferences.Get(UserStorageKeys.SELECTED_LANGUAGE_STORAGE_KEY, null);
        if (selectedCulture == null) LoadDefault();
        else LocalizationService.ChangeCurrentLanguage(selectedCulture);
    }

    public void Save() => 
        Preferences.Set(UserStorageKeys.SELECTED_LANGUAGE_STORAGE_KEY, CultureInfo.CurrentUICulture.TwoLetterISOLanguageName);

    public void LoadDefault() => LocalizationService.ChangeCurrentLanguage(ApplicationCultures.Default);

    public void Clear(bool loadDefault = true)
    {
        Preferences.Remove(UserStorageKeys.SELECTED_LANGUAGE_STORAGE_KEY);
        if (loadDefault) LoadDefault();
    }
}