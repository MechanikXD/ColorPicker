namespace ColorPicker.Models.StaticData;

public static class Pages
{
    public static class Main
    {
        private const string DASHBOARD_PAGE_ROUTE = "main";
        private const string PALETTE_PAGE_ROUTE = "palettes";
        private const string CAMERA_PAGE_ROUTE = "camera";
        private const string HISTORY_PAGE_ROUTE = "history";
        private const string SETTINGS_PAGE_ROUTE = "settings";
        
        public static string Dashboard => DASHBOARD_PAGE_ROUTE;
        public static string Palette => PALETTE_PAGE_ROUTE;
        public static string Camera => CAMERA_PAGE_ROUTE;
        public static string History => HISTORY_PAGE_ROUTE;
        public static string Settings => SETTINGS_PAGE_ROUTE;
        
        private static readonly Dictionary<string, byte> MainPagePosition = new()
        {
            [DASHBOARD_PAGE_ROUTE] = 0,
            [PALETTE_PAGE_ROUTE] = 1,
            [CAMERA_PAGE_ROUTE] = 2,
            [HISTORY_PAGE_ROUTE] = 3,
            [SETTINGS_PAGE_ROUTE] = 4
        };

        public static int GetMainPagePosition(string pageRoute) => 
            MainPagePosition.TryGetValue(pageRoute, out var position) ? position : -1;
    }

    public static class Sub
    {
        private const string MANUAL_COLOR = "manual_color";
        private const string SCAN_RESULT = "scan_result";
        
        public static string ManualColorSelection => MANUAL_COLOR;
        public static string ColorScanResult => SCAN_RESULT;
    }
}