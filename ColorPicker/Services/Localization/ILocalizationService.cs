using System.Globalization;

namespace ColorPicker.Services.Localization;

public interface ILocalizationService
{
    string CurrentCulture { get; }
    void SetCulture(string cultureCode);
}