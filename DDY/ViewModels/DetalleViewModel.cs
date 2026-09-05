using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using DDY.Models;
using DDY.Views;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DDY.ViewModels
{
    [QueryProperty(nameof(Cartas), "Carta")]
    public partial class DetalleViewModel : ObservableObject
    {
        private readonly FavoritosViewModel _favoritosViewModel;

        public DetalleViewModel(FavoritosViewModel favoritosViewModel)
        {
            _favoritosViewModel = favoritosViewModel ?? throw new ArgumentNullException(nameof(favoritosViewModel));
        }

        [ObservableProperty]
        private CartaPokemon cartas;

        [RelayCommand]
        async Task Editar()
        {
            if (Cartas is null)
                return;

            await Shell.Current.GoToAsync(nameof(CartaFormPage), true, new Dictionary<string, object>
            {
                { "Carta", Cartas }
            });
        }

        [RelayCommand]
        private async Task AgregarAFavoritos()
        {
            if (Cartas is null)
                return;

            _favoritosViewModel.AgregarFavorito(Cartas);

            await Shell.Current.DisplayAlert(
                "Favoritos",
                $"{Cartas.Nombre} ha sido agregado a favoritos.",
                "OK");
        }
    }
}
