using CommunityToolkit.Maui;
using CommunityToolkit.Maui.Storage;
using Estimator.UI.Maui.Data;
using Microsoft.Extensions.Logging;

namespace Estimator.UI.Maui
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .UseMauiCommunityToolkit()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                });

            builder.Services.AddMauiBlazorWebView();

#if DEBUG
		builder.Services.AddBlazorWebViewDeveloperTools();
		builder.Logging.AddDebug();
#endif

            builder.Services.AddSingleton<Services.IEstimator, Services.Estimator>();
            builder.Services.AddSingleton<Services.IRateCard, Services.RateCard>();
            builder.Services.AddSingleton<IFolderPicker>(FolderPicker.Default);

            return builder.Build();
        }
    }
}