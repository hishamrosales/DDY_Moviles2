using System.Collections.ObjectModel;
using System.Linq;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using DDY.Models;
using DDY.Services;

namespace DDY.ViewModels
{
    public partial class FavoritosViewModel : ObservableObject
    {
        private readonly CartaApiService _apiService;

        public ObservableCollection<CartaPokemon> Favoritos { get; } = new();

        public FavoritosViewModel(CartaApiService apiService)
        {
            _apiService = apiService;
            CargarFavoritos();
        }

        [RelayCommand]
        public void CargarFavoritos()
        {
            Favoritos.Clear();
            var cartasFavoritas = _apiService.Cartas.Where(c => c.EsFavorito);
            foreach (var carta in cartasFavoritas)
            {
                Favoritos.Add(carta);
            }
        }

        [RelayCommand]
        private void EliminarFavorito(CartaPokemon carta)
        {
            if (carta is null) return;

            carta.EsFavorito = false;
            _apiService.Actualizar(carta);
            Favoritos.Remove(carta);
        }
    }
}