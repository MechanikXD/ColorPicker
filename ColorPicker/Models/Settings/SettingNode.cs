using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace ColorPicker.Models.Settings;

public abstract class SettingNode : INotifyPropertyChanged
{
    public required string Title { get; init; }
    public bool IsEnabled { get; set => SetField(ref field, value); } = true;
    public virtual bool HasChangeableState => true;
    
    public event PropertyChangedEventHandler? PropertyChanged;

    protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    protected bool SetField<T>(ref T field, T value, [CallerMemberName] string? propertyName = null)
    {
        if (EqualityComparer<T>.Default.Equals(field, value)) return false;
        field = value;
        OnPropertyChanged(propertyName);
        return true;
    }
}