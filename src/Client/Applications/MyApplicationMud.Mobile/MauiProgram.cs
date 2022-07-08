using Microsoft.AspNetCore.Components.WebView.Maui;

using MyApplicationMud.Data;

namespace MyApplicationMud;
public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
            });

        builder.Services.AddMauiBlazorWebView();
#if DEBUG
		builder.Services.AddBlazorWebViewDeveloperTools();
#endif
        builder.Services.AddMyApplicationMud("https://localhost:5001");
        return builder.Build();
    }
}
