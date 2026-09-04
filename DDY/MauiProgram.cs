using Microsoft.Extensions.Logging;

namespace DDY
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

            builder.Services.AddTransient<DDY.Views.ListaCartas>();
            builder.Services.AddTransient<DDY.ViewModels.ListaViewModel>();

            builder.Services.AddTransient<DDY.Views.DetalleCarta>();
            builder.Services.AddTransient<DDY.ViewModels.DetalleViewModel>();

            builder.Services.AddTransient<DDY.Views.FavoritosPage>();
            builder.Services.AddTransient<DDY.ViewModels.FavoritosViewModel>();
#endif

            return builder.Build();
        }
    }
}
