using System.Globalization;
using ColorPicker.Resources.Strings;
using ColorPicker.Services.Color;

namespace ColorPicker.Models.History;

public class HistoryEntry
{
    public Color Color { get; init; }
    public HistoryEntrySource Source { get; init; }
    public string Hex { get; } // Hex is the title of the history entry
    public DateTimeOffset RecordDate { get; init; }

    public string HistoryEntryString => GetLocalizedHistorySource();
    public string DateTimeString => GetLocalDateTimeString();
    
    private readonly byte _red;
    private readonly byte _green;
    private readonly byte _blue;

    public byte Red => _red;
    public byte Green => _green;
    public byte Blue => _blue;
    
    private readonly double _hue;
    private readonly double _saturation;
    private readonly double _value;

    public double Hue => _hue;
    public double Saturation => _saturation;
    public double Value => _value;

    public HistoryEntry(Color color, HistoryEntrySource entrySource, DateTimeOffset recordDate)
    {
        Color = color;
        Source = entrySource;
        RecordDate = recordDate;

        Hex = color.ToHex();
        color.ToRgb(out _red, out _green, out _blue);
        color.ToHsv(out _hue, out _saturation, out _value);
    }

    private string GetLocalizedHistorySource()
    {
        return Source switch
        {
            HistoryEntrySource.Scan => AppResources.history_source_scan,
            HistoryEntrySource.Combination => AppResources.history_source_combination,
            _ => Source.ToString()
        };
    }

    private string GetLocalDateTimeString() => 
        RecordDate.ToLocalTime().ToString("g", CultureInfo.InvariantCulture);
}