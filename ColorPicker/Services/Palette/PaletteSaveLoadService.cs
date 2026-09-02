using System.Text.Json;
using ColorPicker.Models.StaticData;
using ColorPicker.Services.SaveLoad;
using ColorPicker.Services.SaveLoad.Defaults;
using ColorPicker.Services.SaveLoad.Serializable;

namespace ColorPicker.Services.Palette;

public class PaletteSaveLoadService : ISaveLoadService
{
    private readonly IPaletteService _paletteService;

    public PaletteSaveLoadService(IPaletteService paletteService)
    {
        _paletteService = paletteService;
    }
    
    public void Load()
    {
        var json = Preferences.Get(UserStorageKeys.PALETTES_STORAGE_KEY, null);
        if (json == null)
        {
            LoadDefault();
            return;
        }

        var data = JsonSerializer.Deserialize(json, AppJsonContext.Default.SerializableUserData) ??
                   DefaultUserPalette.UserData;
        LoadPalettes(data);
    }

    public void Save()
    {
        var serializable = SerializableUserData.FromService(_paletteService);
        var json = JsonSerializer.Serialize(serializable, AppJsonContext.Default.SerializableUserData);
        Preferences.Set(UserStorageKeys.PALETTES_STORAGE_KEY, json);
    }

    public void LoadDefault() => LoadPalettes(DefaultUserPalette.UserData);

    private void LoadPalettes(SerializableUserData userData)
    {
        _paletteService.AllPalettes.Clear();
        foreach (var serializablePalette in userData.Palette) 
            _paletteService.AddPalette(serializablePalette.ToPalette());
        
        _paletteService.SelectPalette(_paletteService.AllPalettes[userData.ActivePaletteIndex]);
        _paletteService.PalettesChanged += Save;
    }

    public void Clear(bool loadDefault=true)
    {
        Preferences.Remove(UserStorageKeys.PALETTES_STORAGE_KEY);
        if (loadDefault) LoadDefault();
    }
}