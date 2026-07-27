using ColorPicker.Models.Colors;

namespace ColorPicker.Services.Color;

public static class ColorCombinationService
{
    private const double COMBINATION_RATIO_STEP = 0.1;
    private const int MAX_COMBINATION_COUNT = 10;
    
    public static IList<ColorCombination> GetCombinations(Microsoft.Maui.Graphics.Color source, ColorPalette palette)
    {
        var resultSet = new List<ColorCombination>();
        if (palette.Palette.Count <= 1) return resultSet;

        var targetLab = CieColorTransformService.RgbToLab(source);
        const int COMBINATIONS_PER_PAIR = (int)(1 / COMBINATION_RATIO_STEP);

        for (var i = 0; i < palette.Palette.Count; i++)
        {
            var color1 = palette.Palette[i];
            // Only unique color combinations 
            for (var j = i + 1; j < palette.Palette.Count; j++)
            {
                var color2 = palette.Palette[j];

                var bestDeltaE = double.MaxValue;
                var bestRatio = 0.0;
                var bestRgb = new Rgb(0, 0, 0);
                
                for (var step = 1; step < COMBINATIONS_PER_PAIR; step++)
                {
                    var ratio = COMBINATION_RATIO_STEP * step;
                    var combinedRed = (int)(color1.Red * ratio + color2.Red * (1 - ratio));
                    var combinedGreen = (int)(color1.Green * ratio + color2.Green * (1 - ratio));
                    var combinedBlue = (int)(color1.Blue * ratio + color2.Blue * (1 - ratio));
                    
                    var combinedColor = new Rgb(combinedRed, combinedGreen, combinedBlue);
                    var combinationDeltaE = CieColorTransformService.GetDeltaE(targetLab, combinedColor.ToColor());
                    if (combinationDeltaE < bestDeltaE)
                    {
                        bestDeltaE = combinationDeltaE;
                        bestRatio = ratio;
                        bestRgb = combinedColor;
                    }
                }
                
                var accuracy = Math.Max(0.0, 1.0 - bestDeltaE / 100.0);
                var combination = new ColorCombination
                {
                    Accuracy = accuracy,
                    FirstColor = color1,
                    FirstColorRatio = bestRatio,
                    ResultColor = ColorSwatch.FromRgb(bestRgb.R, bestRgb.G, bestRgb.B),
                    SecondColor = color2,
                    SecondColorRatio = 1 - bestRatio
                };
                
                // Remove least accurate color from the list, if needed
                if (resultSet.Count < MAX_COMBINATION_COUNT) resultSet.Add(combination);
                else
                {
                    var leastAccurate = resultSet.MinBy(c => c.Accuracy);
                    if (leastAccurate != null && leastAccurate.Accuracy < accuracy)
                    {
                        resultSet.Remove(leastAccurate);
                        resultSet.Add(combination);
                    }
                }
            }
        }

        resultSet.Sort(comparison:(c1, c2) => c2.Accuracy.CompareTo(c1.Accuracy));
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