using DDY.ViewModels;

namespace DDY.Views
{
    public partial class FavoritosPage : ContentPage
    {
        public FavoritosPage()
        {
            InitializeComponent();
            BindingContext = new FavoritosViewModel();
        }
    }
}