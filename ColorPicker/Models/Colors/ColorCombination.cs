using ColorPicker.Resources.Strings;

namespace ColorPicker.Models.Colors;

public class ColorCombination
{
    private const string ACCURACY_LOCALIZATION_KEY = "c_combinations_accuracy";
    
    public ColorSwatch ResultColor { get; set; } = new() { Green = byte.MaxValue };
    public ColorSwatch FirstColor { get; set; } = new() { Red = byte.MaxValue };
    public ColorSwatch SecondColor { get; set; } = new() { Blue = byte.MaxValue };
    public double FirstColorRatio { get; set; } = 0;
    public double SecondColorRatio { get; set; } = 0;
    public double Accuracy { get; set; } = 0;

    public string AccuracyText => $"{AppResources.ResourceManager.GetString(ACCURACY_LOCALIZATION_KEY)}  {0:Accuracy}";
}