using DDY.Views;

namespace DDY
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(FavoritosPage), typeof(FavoritosPage));
            Routing.RegisterRoute(nameof(DetalleCarta), typeof(DetalleCarta));
            Routing.RegisterRoute(nameof(ListaCartas), typeof(ListaCartas));

        }
    }
}
