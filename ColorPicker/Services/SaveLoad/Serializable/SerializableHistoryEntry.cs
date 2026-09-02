using System.Globalization;
using ColorPicker.Models.History;

namespace ColorPicker.Services.SaveLoad.Serializable;

public class SerializableHistoryEntry
{
    public string Hex { get; set; } = "";
    public int EntrySource { get; set; } = 0;
    public string RecordDate { get; set; } = "";
    
    public HistoryEntry ToEntry() =>
        new(Microsoft.Maui.Graphics.Color.FromArgb(Hex), (HistoryEntrySource)EntrySource,
            DateTimeOffset.Parse(RecordDate));

    public static SerializableHistoryEntry FromEntry(HistoryEntry entry) =>
        new()
        {
            Hex = entry.Hex,
            EntrySource = (int)entry.Source,
            RecordDate = entry.RecordDate.ToString("o", CultureInfo.InvariantCulture)
        };
}