using System.Text.Json;
using System.Text.Json.Serialization;
using ColorPicker.Services.Palette;
using ColorPicker.Services.SaveLoad.Defaults;
using ColorPicker.Services.SaveLoad.Serializable;

namespace ColorPicker.Services.SaveLoad;

public class PaletteSaveLoadService : ISaveLoadService
{
    private const string STORAGE_KEY = "user_palette";
    private readonly IPaletteService _paletteService;

    public PaletteSaveLoadService(IPaletteService paletteService)
    {
        _paletteService = paletteService;
    }
    
    public void Load()
    {
        var json = Preferences.Get(STORAGE_KEY, null);
        if (json == null)
        {
            LoadDefault();
            return;
        }
        
        var data = JsonSerializer.Deserialize(json, AppJsonContext.Default.SerializableUserData);
        if (data == null)
        {
            LoadDefault();
            return;
        }
        
        _paletteService.AllPalettes.Clear();
        foreach (var serializablePalette in data.Palette) 
            _paletteService.AddPalette(serializablePalette.ToPalette());
        
        _paletteService.SelectPalette(_paletteService.AllPalettes[data.ActivePaletteIndex]);
    }

    public void Save()
    {
        var serializable = SerializableUserData.FromService(_paletteService);
        var json = JsonSerializer.Serialize(serializable, AppJsonContext.Default.SerializableUserData);
        Preferences.Set(STORAGE_KEY, json);
    }

    public void LoadDefault()
    {
        _paletteService.AllPalettes.Clear();
        foreach (var serializablePalette in DefaultUserPalette.UserData.Palette) 
            _paletteService.AddPalette(serializablePalette.ToPalette());
        
        _paletteService.SelectPalette(_paletteService.AllPalettes[DefaultUserPalette.UserData.ActivePaletteIndex]);
    }

    public void Clear(bool loadDefault=true)
    {
        Preferences.Remove(STORAGE_KEY);
        if (loadDefault) LoadDefault();
    }
}

[JsonSourceGenerationOptions(
    WriteIndented = false,
    DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
    PropertyNamingPolicy = JsonKnownNamingPolicy.CamelCase
)]
[JsonSerializable(typeof(SerializableUserData))]
public partial class AppJsonContext : JsonSerializerContext;