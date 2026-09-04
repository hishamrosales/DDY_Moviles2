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
#endif

            builder.Services.AddTransient<DDY.Views.ListaCartas>();
            // Singleton: así, al agregar una carta desde el Formulario, se refleja
            // en la misma lista que ya está en pantalla, sin recrearla desde cero.
            builder.Services.AddSingleton<DDY.ViewModels.ListaViewModel>();

            builder.Services.AddTransient<DDY.Views.DetalleCarta>();
            builder.Services.AddTransient<DDY.ViewModels.DetalleViewModel>();

            builder.Services.AddTransient<DDY.Views.FavoritosPage>();
            builder.Services.AddTransient<DDY.ViewModels.FavoritosViewModel>();

            builder.Services.AddTransient<DDY.Views.CartaFormPage>();
            builder.Services.AddTransient<DDY.ViewModels.CartaFormViewModel>();

            return builder.Build();
        }
    }
}