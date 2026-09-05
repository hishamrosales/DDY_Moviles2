using Microsoft.Extensions.Logging;

namespace DDY
{
    public static class MauiProgram
    {
        // Necesario porque ListaCartas se muestra vía ShellContent.ContentTemplate,
        // y ese mecanismo de MAUI NO soporta inyección por constructor (a diferencia
        // de las páginas a las que se navega con Shell.Current.GoToAsync).
        public static IServiceProvider Services { get; private set; } = default!;

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

            var app = builder.Build();
            Services = app.Services;
            return app;
        }
    }
}