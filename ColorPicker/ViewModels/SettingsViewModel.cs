using System.Windows.Input;
using ColorPicker.Models.Settings;
using ColorPicker.Models.Settings.Nodes;
using ColorPicker.Services.SaveLoad;
using ColorPicker.Services.Settings;

namespace ColorPicker.ViewModels;

public class SettingsViewModel : BaseViewModel
{
    public const string SETTING_SAVE_LOAD_SERVICE_KEY = "settings_save_load_service_key";
    
    private readonly Stack<(string Title, IReadOnlyList<SettingNode> Nodes)> _stack = new();

    public IReadOnlyList<SettingNode> CurrentNodes
    {
        get;
        private set => SetField(ref field, value);
    } = [];

    public string CurrentTitle
    {
        get;
        private set => SetField(ref field, value);
    } = "settings_title";

    public bool CanGoBack => _stack.Count > 0;

    public ICommand NavigateToGroupCommand { get; }
    public ICommand GoBackCommand          { get; }
    
    public SettingsViewModel([FromKeyedServices(SETTING_SAVE_LOAD_SERVICE_KEY)] ISaveLoadService settingsSaveLoadService)
    {
        NavigateToGroupCommand = new Command<GroupSetting>(group =>
        {
            if (group is null) return;
            _stack.Push((CurrentTitle, CurrentNodes));
            CurrentTitle = group.Title;
            CurrentNodes = group.Children;
            OnPropertyChanged(nameof(CanGoBack));
        });

        GoBackCommand = new Command(_ =>
        {
            if (_stack.Count == 0) return;
            var (title, nodes) = _stack.Pop();
            CurrentTitle = title;
            CurrentNodes = nodes;
            OnPropertyChanged(nameof(CanGoBack));
        });
        
        if (settingsSaveLoadService is SettingsSaveLoadService saveLoadService) LoadSettings(saveLoadService);
    }

    private void LoadSettings(SettingsSaveLoadService saveLoadService)
    {
        CurrentNodes = SettingsLayout.GetLayout();
        saveLoadService.ParseSettingGroup(CurrentNodes, []);
        saveLoadService.Load();
    }

    public bool TryGoBack()
    {
        if (!CanGoBack) return false;
        GoBackCommand.Execute(null);
        return true;
    }
}