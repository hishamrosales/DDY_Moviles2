using CommunityToolkit.Mvvm.ComponentModel;

namespace DDY.Models
{
    public partial class CartaPokemon : ObservableObject
    {
        [ObservableProperty]
        private string id = string.Empty;

        [ObservableProperty]
        private string nombre = string.Empty;

        [ObservableProperty]
        private string categoria = string.Empty;

        [ObservableProperty]
        private string tipo = string.Empty;

        [ObservableProperty]
        private string rareza = string.Empty;

        [ObservableProperty]
        private string estado = string.Empty;

        [ObservableProperty]
        private decimal valorEstimado;

        [ObservableProperty]
        private string imagen = string.Empty;

        [ObservableProperty]
        private bool esFavorito;

        [ObservableProperty]
        private bool esLocal;
    }
}