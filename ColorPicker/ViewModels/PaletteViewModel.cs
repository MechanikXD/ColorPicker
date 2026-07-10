using System.Collections.ObjectModel;
using System.Windows.Input;
using ColorPicker.Models.Colors;
using ColorPicker.Services.Palette;

namespace ColorPicker.ViewModels;

public class PaletteViewModel : BaseViewModel
{
    private readonly IPaletteService _paletteService;

    public ColorPalette? CurrentPalette
    {
        get;
        private set => SetField(ref field, value);
    }

    public int CurrentPaletteIndex
    {
        get;
        private set => SetField(ref field, value);
    }

    public ICommand AddPaletteCommand { get; }
    public ICommand RenamePaletteCommand { get; }
    public ICommand DeletePaletteCommand { get; }
    public ICommand SelectPaletteCommand { get; }
    public ICommand AddColorCommand { get; }
    public ICommand AddColorManuallyCommand { get; }
    public ICommand AddColorFromCameraCommand { get; }
    public ICommand EditColorCommand { get; }
    public ICommand DeleteColorCommand { get; }

    public PaletteViewModel(IPaletteService paletteService)
    {
        _paletteService = paletteService;
        _paletteService.CurrentPaletteChanged += () =>
        {
            CurrentPalette = _paletteService.CurrentPalette;
            CurrentPaletteIndex = paletteService.CurrentPaletteIndex;
        };

        CurrentPalette = _paletteService.CurrentPalette;
        CurrentPaletteIndex = 0;

        AddPaletteCommand = new Command(_ =>
        {
            /* prompt name → _paletteService.AddPalette */
        });
        RenamePaletteCommand = new Command(_ =>
        {
            /* prompt new name → _paletteService.RenamePalette */
        });
        DeletePaletteCommand = new Command(_ =>
        {
            /* confirm → _paletteService.RemovePalette */
        });

        SelectPaletteCommand = new Command<ColorPalette>(p =>
        {
            if (p is not null) _paletteService.SelectPalette(p);
        });

        // Opens an ActionSheet "Manual / Camera"; routes accordingly.
        AddColorCommand = new Command(_ =>
        {
            /* Shell.Current.DisplayActionSheet → GoToAsync */
        });

        AddColorManuallyCommand = new Command(_ => { Shell.Current.GoToAsync("manualcolor?isEditMode=false"); });

        AddColorFromCameraCommand = new Command(_ =>
        {
            // Shell.Current.GoToAsync("camera") — capture flow returns color to service
        });

        EditColorCommand = new Command<ColorSwatch>(swatch =>
        {
            if (swatch is null) return;
            Shell.Current.GoToAsync($"manualcolor?isEditMode=true&colorHex={swatch.Hex.TrimStart('#')}");
        });

        DeleteColorCommand = new Command<ColorSwatch>(swatch =>
        {
            if (swatch is not null) _paletteService.RemoveColor(swatch);
        });
    }
}