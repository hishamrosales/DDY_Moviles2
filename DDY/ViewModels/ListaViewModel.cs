using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using DDY.Models;
using DDY.Services;
using DDY.Views;
using Microsoft.Maui.Controls;

namespace DDY.ViewModels
{
    public partial class ListaViewModel : ObservableObject
    {
        private readonly CartaApiService _apiService;

        public CartaApiService ApiService => _apiService;

        public ObservableCollection<CartaPokemon> Cartas => _apiService.Cartas;

        public ListaViewModel(CartaApiService apiService)
        {
            _apiService = apiService;
            _ = CargarCartas();
        }

        [RelayCommand]
        public async Task CargarCartas()
        {
            await _apiService.CargarCartasInicialesAsync(forzarRecarga: true);
        }

        [RelayCommand]
        private async Task VerDetalle(CartaPokemon carta)
        {
            if (carta is null) return;

            
            await Shell.Current.GoToAsync(nameof(DetalleCarta), new Dictionary<string, object>
            {
                { "Carta", carta }
            });
        }

        [RelayCommand]
        private async Task IrAAgregar()
        {
            await Shell.Current.GoToAsync(nameof(CartaFormPage));
        }

        [RelayCommand]
        private async Task IrAFavoritos()
        {
            await Shell.Current.GoToAsync(nameof(FavoritosPage));
        }

        [RelayCommand]
        private async Task EliminarCarta(CartaPokemon carta)
        {
            if (carta is null) return;

            bool confirmar = await Shell.Current.DisplayAlert("Confirmar", $"¿Deseas eliminar a {carta.Nombre}?", "Sí", "No");
            if (confirmar)
            {
                _apiService.Eliminar(carta);
            }
        }
    }
}