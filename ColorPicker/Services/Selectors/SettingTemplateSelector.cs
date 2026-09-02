using ColorPicker.Models.Settings.Nodes;

namespace ColorPicker.Services.Selectors;

public class SettingTemplateSelector : DataTemplateSelector
{
    public DataTemplate? TitleTemplate { get; set; }
    public DataTemplate? GroupTemplate { get; set; }
    public DataTemplate? ToggleTemplate { get; set; }
    public DataTemplate? DropdownTemplate { get; set; }

    protected override DataTemplate? OnSelectTemplate(object item, BindableObject container)
    {
        return item switch
        {
            TitleSetting => TitleTemplate,
            GroupSetting => GroupTemplate,
            ToggleSetting => ToggleTemplate,
            DropDownSetting => DropdownTemplate,
            _ => null
        };
    }
}