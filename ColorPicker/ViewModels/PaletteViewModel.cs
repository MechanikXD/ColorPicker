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
        AddPaletteCommand = new Command(_ =>
        {
            _paletteService.AddPalette(new ColorPalette { Title = "Test" });
            NotifyProperty();
        });
        RenamePaletteCommand = new Command(_ =>
        {
            if (CurrentPalette != null)
            {
                _paletteService.RenamePalette(CurrentPalette, "Renamed Test");
                NotifyProperty();
            }
        });
        DeletePaletteCommand = new Command(_ =>
        {
            if (CurrentPalette != null)
            {
                _paletteService.RemovePalette(CurrentPalette);
                NotifyProperty();
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

    private void NotifyProperty()
    {
        OnPropertyChanged(nameof(AllPalettes));
        OnPropertyChanged(nameof(CurrentPalette));
        OnPropertyChanged(nameof(CurrentPalette));
        OnPropertyChanged(nameof(CurrentPaletteIndex));
    }
}