using ColorPicker.Models.Colors;

namespace ColorPicker.Services.Color;

public static class ColorCombinationService
{
    private const double COMBINATION_RATIO_STEP = 0.1;
    private const int MAX_COMBINATION_COUNT = 10;
    
    public static IList<ColorCombination> GetCombinations(Microsoft.Maui.Graphics.Color source, ColorPalette palette)
    {
        var resultSet = new List<ColorCombination>(Math.Min(MAX_COMBINATION_COUNT,
            palette.Palette.Count * (palette.Palette.Count - 1)));

        var targetLab = CieColorTransformService.RgbToLab(source);
        var targetRgb = Rgb.FromColor(source);
        var targetSwatch = ColorSwatch.FromColor(source);
        
        foreach (var color1 in palette.Palette)
        {
            foreach (var color2 in palette.Palette)
            {
                for (var ratio = COMBINATION_RATIO_STEP; ratio < 1; ratio += COMBINATION_RATIO_STEP)
                {
                    var combinedRed = (int)(color1.Red * ratio + color2.Red * (1 - ratio));
                    var combinedGreen = (int)(color1.Green * ratio + color2.Green * (1 - ratio));
                    var combinedBlue = (int)(color1.Blue * ratio + color2.Blue * (1 - ratio));
                    var combinedColor = new Rgb(combinedRed, combinedGreen, combinedBlue);

                    var accuracy = 1.0;
                    if (targetRgb.Equals(combinedColor))
                    {
                        if (resultSet.Count >= resultSet.Capacity)
                        {
                            var leastAccurate = resultSet.MinBy(combination => combination.Accuracy);
                            if (leastAccurate != null) resultSet.Remove(leastAccurate);
                        }
                    }
                    else
                    {
                        accuracy = CieColorTransformService.GetDeltaE(targetLab, combinedColor.ToColor());
                        if (resultSet.Count >= resultSet.Capacity)
                        {
                            var leastAccurate = resultSet.MinBy(combination => combination.Accuracy);
                            if (leastAccurate != null && leastAccurate.Accuracy < accuracy) resultSet.Remove(leastAccurate);
                            else continue;
                        }
                    }

                    resultSet.Add(new ColorCombination
                    {
                        Accuracy = accuracy,
                        FirstColor = color1,
                        FirstColorRatio = ratio,
                        ResultColor = targetSwatch,
                        SecondColor = color2,
                        SecondColorRatio = 1 - ratio
                    });
                }
            }
        }
        
        return resultSet;
    }

    private record Rgb(int R, int G, int B)
    {
        public static Rgb FromColor(Microsoft.Maui.Graphics.Color color) => 
            new((int)(color.Red * 255), (int)(color.Green * 255), (int)(color.Blue * 255));

        public Microsoft.Maui.Graphics.Color ToColor() => 
            Microsoft.Maui.Graphics.Color.FromRgb(R / 255.0, G / 255.0, B / 255.0);

        public virtual bool Equals(Rgb? other)
        {
            if (other == null) return false;
            return R == other.B && G == other.G && B == other.B;
        }

        public override int GetHashCode() => HashCode.Combine(R, G, B);
    }
}