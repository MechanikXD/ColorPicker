using System.Windows.Input;
using ColorPicker.Models.Settings;
using ColorPicker.Models.Settings.Nodes;

namespace ColorPicker.ViewModels;

public class SettingsViewModel : BaseViewModel
{
    public static Dictionary<string, SettingNode> Settings { get; private set; } = [];
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
    } = "Settings";

    public bool CanGoBack => _stack.Count > 0;

    public ICommand NavigateToGroupCommand { get; }
    public ICommand GoBackCommand          { get; }
    
    public SettingsViewModel()
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
        
        LoadSettings();
    }

    private void LoadSettings()
    {
        CurrentNodes = SettingsLayout.GetLayout();
        ParseSettingGroup(CurrentNodes, []);
    }

    private void ParseSettingGroup(IReadOnlyList<SettingNode> nodes, HashSet<GroupSetting> navigatedSet)
    {
        foreach (var node in nodes)
        {
            if (node is GroupSetting group && navigatedSet.Add(group)) ParseSettingGroup(group.Children, navigatedSet);
            else if (node.HasChangeableState) Settings.Add(node.Title, node);
        }
    }

    public bool TryGoBack()
    {
        if (!CanGoBack) return false;
        GoBackCommand.Execute(null);
        return true;
    }
}