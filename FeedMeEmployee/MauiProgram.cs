using FeedMeEmployee.HttpCall;
using Microsoft.Extensions.Logging;
using FeedMeEmployee.Business.Interface;
using FeedMeEmployee.ViewModels;
using TodoREST.Services;
using FeedMeEmployee.Views;
//#if ANDROID || iOS
//using FeedMeEmployee.Platforms;
//#endif

namespace FeedMeEmployee
{
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
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

#if DEBUG
    		builder.Logging.AddDebug();
#endif
            //#if ANDROID || iOS
            //        builder.Services.AddTransient<INfcService, NfcService>();
            //#endif
            builder.Services.AddSingleton<IHttpsClientHandlerService, HttpsClientHandlerService>();
            builder.Services.AddSingleton<IRestService, RestService>();
            builder.Services.AddSingleton<Historique>();
            builder.Services.AddSingleton<ComptePage>();

            return builder.Build();
        }
    }
}
