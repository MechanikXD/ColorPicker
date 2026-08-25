using System.Globalization;
using ColorPicker.Resources.Strings;

namespace ColorPicker.Services.Localization;

[ContentProperty(nameof(Text))]
public class TranslateExtension : IMarkupExtension
{
    private const string ABSENT_KEY_MESSAGE = "NO_LOCALIZATION_KEY_FOUND";
    public string Text { get; set; } = string.Empty;

    public object ProvideValue(IServiceProvider serviceProvider)
    {
        if (string.IsNullOrWhiteSpace(Text))
            return string.Empty;

        var translation = AppResources.ResourceManager.GetString(Text, CultureInfo.CurrentUICulture);

        return translation ?? ABSENT_KEY_MESSAGE; // Fallback to key name if string isn't found
    }

}