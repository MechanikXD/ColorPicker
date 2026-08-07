using System.Collections.ObjectModel;
using System.Windows.Input;
using ColorPicker.Models.Colors;
using ColorPicker.Services.Navigation;
using ColorPicker.Services.Palette;

namespace ColorPicker.ViewModels;

public class PaletteViewModel : BaseViewModel
{
    private readonly IPaletteService _paletteService;
    
    public ObservableCollection<ColorPalette> AllPalettes => _paletteService.AllPalettes;
    public List<string> PaletteNames { get; private set; }
    public string CurrentPaletteTitle => CurrentPalette?.Title ?? "None";

    public ColorPalette? CurrentPalette
    {
        get;
        private set
        {
            if (SetField(ref field, value)) OnPropertyChanged(nameof(CurrentPaletteTitle));
        }
    }

    public int CurrentPaletteIndex
    {
        get;
        set
        {
            if (SetField(ref field, value) && value >= 0 && value < AllPalettes.Count)
                _paletteService.SelectPalette(AllPalettes[value]);
        }
    }

    public PromptViewModel Prompt { get; }

    public ICommand AddPaletteCommand { get; }
    public ICommand RenamePaletteCommand { get; }
    public ICommand DeletePaletteCommand { get; }
    public ICommand AddColorManuallyCommand { get; }
    public ICommand AddColorFromCameraCommand { get; }
    public ICommand EditColorCommand { get; }
    public ICommand DeleteColorCommand { get; }

    public PaletteViewModel(IPaletteService paletteService, PromptViewModel prompt)
    {
        _paletteService = paletteService;
        Prompt = prompt;
        _paletteService.CurrentPaletteChanged += RefreshPalette;
        CurrentPalette = _paletteService.CurrentPalette;
        CurrentPaletteIndex = _paletteService.CurrentPaletteIndex;
        PaletteNames = AllPalettes.Select(p => p.Title).ToList();
        
        AddPaletteCommand = new Command(_ =>
        {
            AskTitle(() =>
            {
                _paletteService.AddPalette(new ColorPalette { Title = Prompt.InputText });
                RefreshPalette();
            });
        });
        RenamePaletteCommand = new Command(_ =>
        {
            if (CurrentPalette != null)
            {
                AskTitle(() =>
                {
                    _paletteService.RenamePalette(CurrentPalette, Prompt.InputText);
                    RefreshPalette();
                    OnPropertyChanged(nameof(CurrentPaletteTitle));
                });
            }
        });
        DeletePaletteCommand = new Command(_ =>
        {
            if (CurrentPalette != null)
            {
                var target = CurrentPalette;
                ConfirmDeletion(CurrentPaletteTitle, () =>
                {
                    _paletteService.RemovePalette(target);
                    RefreshPalette();
                });
            }
        });

        AddColorManuallyCommand = new Command(_ => { ShellNavigationService.DoToSubPage("manualcolor", 
            new Dictionary<string, object> { ["isEditMode"] = false}); });
        AddColorFromCameraCommand = new Command(_ => { ShellNavigationService.GoToPage("camera"); });

        EditColorCommand = new Command<ColorSwatch>(swatch =>
        {
            if (swatch is null) return;
            ShellNavigationService.DoToSubPage("manualcolor",
                new Dictionary<string, object>
                    { ["isEditMode"] = true, ["colorHex"] = swatch.Hex.TrimStart('#') });
        });

        DeleteColorCommand = new Command<ColorSwatch>(swatch =>
        {
            if (swatch is not null)
            {
                var swatchName = string.IsNullOrEmpty(swatch.Name) || string.IsNullOrWhiteSpace(swatch.Name)
                    ? swatch.Hex
                    : swatch.Name;
                ConfirmDeletion(swatchName, () =>
                {
                    _paletteService.RemoveColor(swatch);
                });
            }
        });
    }

    private void ConfirmDeletion(string targetName, Action confirmAction)
    {
        Prompt.Show(
            title: $"Delete {targetName}?",
            showMessage: false,
            isDestructive: true,
            onConfirm: confirmAction
        );
    }

    private void AskTitle(Action confirmAction)
    {
        Prompt.Show(
            title: "Enter palette title",
            message: "Name your palette so you can find it later",
            inputHint: "Palette's name",
            showInput: true,
            onConfirm: confirmAction
        );
    }
    
    private void RefreshPalette()
    {
        CurrentPalette = _paletteService.CurrentPalette;
        PaletteNames = AllPalettes.Select(p => p.Title).ToList();
        OnPropertyChanged(nameof(PaletteNames));
        CurrentPaletteIndex = _paletteService.CurrentPaletteIndex;
    }
}