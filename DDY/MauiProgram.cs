using DDY.Services;
using DDY.ViewModels;
using Microsoft.Extensions.Logging;

namespace DDY
{
    public static class MauiProgram
    {
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

            // Servicios / Repositorio
            builder.Services.AddSingleton<CartaApiService>();
            

            // Páginas -> Transient
            builder.Services.AddTransient<DDY.Views.ListaCartas>();
            builder.Services.AddTransient<DDY.Views.DetalleCarta>();
            builder.Services.AddTransient<DDY.Views.FavoritosPage>();
            builder.Services.AddTransient<DDY.Views.CartaFormPage>();

            // ViewModels
            builder.Services.AddTransient<ListaViewModel>();
            builder.Services.AddTransient<DetalleViewModel>();
            builder.Services.AddSingleton<FavoritosViewModel>();
            builder.Services.AddTransient<CartaFormViewModel>();

            var app = builder.Build();
            Services = app.Services;
            return app;
        }
    }
}