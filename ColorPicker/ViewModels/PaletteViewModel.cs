using System.Collections.ObjectModel;
using System.Windows.Input;
using ColorPicker.Models.Colors;
using ColorPicker.Services.Palette;
using CommunityToolkit.Maui.Core.Extensions;

namespace ColorPicker.ViewModels;

public class PaletteViewModel : BaseViewModel
{
    private readonly IPaletteService _paletteService;

    public ObservableCollection<ColorPalette> AllPalettes => _paletteService.AllPalettes.ToObservableCollection();
    public string CurrentPaletteTitle => CurrentPalette == null ? "No Palette" : CurrentPalette.Title;
    public ColorPalette? CurrentPalette => _paletteService.CurrentPalette;
    public int CurrentPaletteIndex => _paletteService.CurrentPaletteIndex;
    
    public PromptViewModel Prompt { get; }

    public ICommand AddPaletteCommand { get; }
    public ICommand RenamePaletteCommand { get; }
    public ICommand DeletePaletteCommand { get; }
    public ICommand SelectPaletteCommand { get; }
    public ICommand AddColorManuallyCommand { get; }
    public ICommand AddColorFromCameraCommand { get; }
    public ICommand EditColorCommand { get; }
    public ICommand DeleteColorCommand { get; }

    public PaletteViewModel(IPaletteService paletteService, PromptViewModel prompt)
    {
        _paletteService = paletteService;
        Prompt = prompt;
        AddPaletteCommand = new Command(_ =>
        {
            AskTitle(() =>
            {
                _paletteService.AddPalette(new ColorPalette { Title = Prompt.InputText });
                NotifyProperty();
            });
        });
        RenamePaletteCommand = new Command(_ =>
        {
            if (CurrentPalette != null)
            {
                AskTitle(() =>
                {
                    _paletteService.RenamePalette(CurrentPalette, Prompt.InputText);
                    NotifyProperty();
                });
            }
        });
        DeletePaletteCommand = new Command(_ =>
        {
            if (CurrentPalette != null)
            {
                ConfirmDeletion(CurrentPaletteTitle, () =>
                {
                    _paletteService.RemovePalette(CurrentPalette);
                    NotifyProperty();
                });
            }
        });

        SelectPaletteCommand = new Command<ColorPalette>(p =>
        {
            if (p is not null)
            {
                _paletteService.SelectPalette(p);
                NotifyProperty();
            }
        });

        AddColorManuallyCommand = new Command(_ => { Shell.Current.GoToAsync("manualcolor?isEditMode=false"); });
        AddColorFromCameraCommand = new Command(_ => { Shell.Current.GoToAsync("camera"); });

        EditColorCommand = new Command<ColorSwatch>(swatch =>
        {
            if (swatch is null) return;
            Shell.Current.GoToAsync($"manualcolor?isEditMode=true&colorHex={swatch.Hex.TrimStart('#')}");
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
                    NotifyProperty();
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

    private void NotifyProperty()
    {
        OnPropertyChanged(nameof(AllPalettes));
        OnPropertyChanged(nameof(CurrentPaletteTitle));
        OnPropertyChanged(nameof(CurrentPalette));
        OnPropertyChanged(nameof(CurrentPaletteIndex));
    }
}