using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using DDY.Models;

namespace DDY.ViewModels
{
    public partial class FavoritosViewModel : ObservableObject
    {
        public ObservableCollection<CartaPokemon> Favoritos { get; } = new();

        public void AgregarFavorito(CartaPokemon carta)
        {
            if (carta is null) return;

            carta.EsFavorito = true;

            if (!Favoritos.Contains(carta))
                Favoritos.Add(carta);
        }

        [RelayCommand]
        private void EliminarFavorito(CartaPokemon carta)
        {
            if (carta is null) return;

            carta.EsFavorito = false;
            Favoritos.Remove(carta);
        }
    }
}