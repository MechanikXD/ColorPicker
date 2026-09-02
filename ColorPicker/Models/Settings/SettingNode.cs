using System.ComponentModel;
using System.Runtime.CompilerServices;
using ColorPicker.Services.Localization;
using CommunityToolkit.Mvvm.Messaging;

namespace ColorPicker.Models.Settings;

public abstract class SettingNode : INotifyPropertyChanged
{
    public required string Id { get; init; }
    public string Title { get; set; } = "";
    public required Action<SettingNode> RefreshLocalization;
    
    public event PropertyChangedEventHandler? PropertyChanged;

    protected SettingNode()
    {
        WeakReferenceMessenger.Default.Register<LocalizationService.CultureChangedMessage>(this,
            (_, _) => RefreshSettingLocalization());
    }

    public void RefreshSettingLocalization() => RefreshLocalization(this);

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