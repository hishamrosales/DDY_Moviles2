using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using DDY.Models;
using DDY.Services;
using DDY.Views;
using Microsoft.Maui.Controls;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DDY.ViewModels
{
    [QueryProperty(nameof(Carta), "Carta")]
    [QueryProperty(nameof(Carta), "carta")]
    public partial class DetalleViewModel : ObservableObject, IQueryAttributable
    {
        private readonly CartaApiService _apiService;

        [ObservableProperty]
        private CartaPokemon? carta;

        [ObservableProperty]
        private string iconoFavorito = "star_outline.png";

        public DetalleViewModel(CartaApiService apiService)
        {
            _apiService = apiService;
        }

        public void ApplyQueryAttributes(IDictionary<string, object> query)
        {
            if (query == null || query.Count == 0) return;

            if (query.TryGetValue("Carta", out var param) || query.TryGetValue("carta", out param))
            {
                if (param is CartaPokemon cartaEncontrada)
                {
                    Carta = cartaEncontrada;
                }
            }

            ActualizarIconoFavorito();
        }

        partial void OnCartaChanged(CartaPokemon? value)
        {
            ActualizarIconoFavorito();
        }

        [RelayCommand]
        private async Task ToggleFavoritoAsync()
        {
            if (Carta is null) return;

            
            Carta.EsFavorito = !Carta.EsFavorito;

            
            _apiService.Actualizar(Carta);

            
            ActualizarIconoFavorito();

            
            string mensaje = Carta.EsFavorito
                ? $"¡'{Carta.Nombre}' fue agregada a tus favoritos!"
                : $"'{Carta.Nombre}' fue eliminada de tus favoritos.";

            await Shell.Current.DisplayAlert("Favoritos", mensaje, "OK");
        }

        private void ActualizarIconoFavorito()
        {
            IconoFavorito = (Carta != null && Carta.EsFavorito) ? "star_filled.png" : "star_outline.png";
        }

        [RelayCommand]
        private async Task IrAEditarAsync()
        {
            if (Carta == null)
            {
                await Shell.Current.DisplayAlert("Aviso", "No se encontró la carta para editar.", "OK");
                return;
            }

            await Shell.Current.GoToAsync(nameof(CartaFormPage), new Dictionary<string, object>
            {
                { "Carta", Carta }
            });
        }

        [RelayCommand]
        private async Task EliminarAsync()
        {
            if (Carta == null) return;

            bool confirmar = await Shell.Current.DisplayAlert(
                "Confirmar",
                $"¿Seguro que deseas eliminar a '{Carta.Nombre}'?",
                "Sí, eliminar",
                "Cancelar");

            if (confirmar)
            {
                _apiService.Eliminar(Carta);
                await Shell.Current.GoToAsync("..");
            }
        }
    }
}