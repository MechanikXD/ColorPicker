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
        var labColors = new Dictionary<ColorSwatch, LabColor>();
        const int COMBINATIONS_PER_PAIR = (int)(1 / COMBINATION_RATIO_STEP);

        for (var i = 0; i < palette.Palette.Count; i++)
        {
            if (!labColors.TryGetValue(palette.Palette[i], out var lab1))
            {
                lab1 = CieColorTransformService.RgbToLab(palette.Palette[i].ToColor);
                labColors.Add(palette.Palette[i], lab1);
            }
            // Only unique color combinations 
            for (var j = i + 1; j < palette.Palette.Count; j++)
            {
                if (!labColors.TryGetValue(palette.Palette[j], out var lab2))
                {
                    lab2 = CieColorTransformService.RgbToLab(palette.Palette[j].ToColor);
                    labColors.Add(palette.Palette[j], lab2);
                }
                
                var bestDeltaE = double.MaxValue;
                var bestRatio = 0.0;
                var bestLab = new LabColor(0, 0, 0);
                
                for (var step = 1; step < COMBINATIONS_PER_PAIR; step++)
                {
                    var ratio = COMBINATION_RATIO_STEP * step;
                    
                    var mixedL = lab1.L * ratio + lab2.L * (1.0 - ratio);
                    var mixedA = lab1.A * ratio + lab2.A * (1.0 - ratio);
                    var mixedB = lab1.B * ratio + lab2.B * (1.0 - ratio);
                    
                    var mixedLab = new LabColor(mixedL, mixedA, mixedB);
                    var combinationDeltaE = CieColorTransformService.GetDeltaE(targetLab, mixedLab);
                    
                    if (combinationDeltaE < bestDeltaE)
                    {
                        bestDeltaE = combinationDeltaE;
                        bestRatio = ratio;
                        bestLab = mixedLab;
                    }
                }
                
                var accuracy = Math.Max(0.0, 1.0 - bestDeltaE / 100.0);
                var combination = new ColorCombination
                {
                    Accuracy = accuracy,
                    FirstColor = ColorSwatch.FromColor(CieColorTransformService.LabToRgb(lab1)),
                    FirstColorRatio = bestRatio,
                    ResultColor = ColorSwatch.FromColor(CieColorTransformService.LabToRgb(bestLab)),
                    SecondColor = ColorSwatch.FromColor(CieColorTransformService.LabToRgb(lab2)),
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
}