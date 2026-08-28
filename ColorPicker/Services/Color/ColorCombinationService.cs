using System.Collections.Concurrent;
using ColorPicker.Models.Colors;
using ColorPicker.Models.Settings;

namespace ColorPicker.Services.Color;

public static class ColorCombinationService
{
    public static async Task<IList<ColorCombination>> GetCombinationsAsync(Microsoft.Maui.Graphics.Color source, 
        ColorPalette palette, CancellationToken ct = default)
    {
        if (palette.Palette.Count <= 1) return [];
        var combinationRatioStep = double.Parse(SettingsModels.ColorSettings.CombinationRatioStep.GetCurrentOption());
        var maxCombinationCount = int.Parse(SettingsModels.ColorSettings.MaxCombinationCount.GetCurrentOption());

        return await Task.Run(() =>
        {
            var targetLab = CieColorTransformService.RgbToLab(source);
            var combinationsPerPair = (int)(1 / combinationRatioStep);
            
            var paletteList = palette.Palette;
            var totalCount = paletteList.Count;
            var bag = new ConcurrentBag<ColorCombination>();

            Parallel.For(0, totalCount, new ParallelOptions { CancellationToken = ct }, i =>
            {
                var color1 = palette.Palette[i];
                // Only unique color combinations 
                for (var j = i + 1; j < palette.Palette.Count; j++)
                {
                    var color2 = palette.Palette[j];

                    var bestDeltaE = double.MaxValue;
                    var bestRatio = 0.0;
                    var bestRgb = new Rgb(0, 0, 0);

                    for (var step = 1; step < combinationsPerPair; step++)
                    {
                        var ratio = combinationRatioStep * step;
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
                    bag.Add(combination);
                }
            });

            return bag.OrderByDescending(c => c.Accuracy).Take(maxCombinationCount).ToList();
        }, ct);
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