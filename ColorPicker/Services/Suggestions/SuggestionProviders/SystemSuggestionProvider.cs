using ColorPicker.Models.Suggestion;
using ColorPicker.View;

namespace ColorPicker.Services.Suggestions.SuggestionProviders;

public class SystemSuggestionProvider : ISuggestionProvider
{
    private CameraPage _cameraPage;
    
    public SystemSuggestionProvider(CameraPage cameraPage)
    {
        _cameraPage = cameraPage;
    }
    
    public async Task<IReadOnlyList<SuggestionMessage>> GetSuggestions()
    {
        return !await _cameraPage.HasValidCameraPermission()
            ? [SuggestionModels.SystemSuggestions.NoCameraPermission]
            : [];
    }
}