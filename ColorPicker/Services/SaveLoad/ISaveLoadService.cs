namespace ColorPicker.Services.SaveLoad;

public interface ISaveLoadService
{
    public void Load();
    public void Save();
    public void LoadDefault();
    public void Clear(bool loadDefault=true);
}