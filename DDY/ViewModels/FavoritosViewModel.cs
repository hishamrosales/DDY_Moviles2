using DDY.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using System.Linq;

namespace DDY.ViewModels
{
    public partial class FavoritosViewModel : ObservableObject
    {
        [ObservableProperty]
        private ObservableCollection<CartaPokemon> favoritos = new();

        [RelayCommand]
        private void EliminarFavorito(CartaPokemon carta)
        {
            if (carta is null)
                return;

            Favoritos.Remove(carta);
            carta.EsFavorito = false;
        }

        public void AgregarFavorito(CartaPokemon carta)
        {
            if (carta is null)
                return;

            if (!Favoritos.Any(c => c.Nombre == carta.Nombre))
            {
                Favoritos.Add(carta);
                carta.EsFavorito = true;
            }
        }
    }
}