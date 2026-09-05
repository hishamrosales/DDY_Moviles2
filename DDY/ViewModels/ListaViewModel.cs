using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using DDY.Models;
using DDY.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace DDY.ViewModels
{
    public partial class ListaViewModel : ObservableObject
    {
        private readonly ListaCartas listaCartas;

        [ObservableProperty]
        private ObservableCollection<CartaPokemon> cartas = new();

        public ListaViewModel()
        {
            CargarCartas();
        }

        [RelayCommand]
        async Task CargarCartas()
        {
            Cartas = new ObservableCollection<CartaPokemon> {
            new CartaPokemon
            {
                Nombre = "Pikachu",
                Categoria = "Pokémon",
                Tipo = "Eléctrico",
                Rareza = "Rara",
                Estado = "Buena",
                ValorEstimado = 250,
                Imagen = "dotnet_bot.png",
                EsFavorito = true
            },

            new CartaPokemon
            {
                Nombre = "Charizard",
                Categoria = "Pokémon",
                Tipo = "Fuego",
                Rareza = "Ultra Rara",
                Estado = "Nueva",
                ValorEstimado = 1500,
                Imagen = "dotnet_bot.png",
                EsFavorito = false
            },

            new CartaPokemon
            {
                Nombre = "Mew",
                Categoria = "Pokémon",
                Tipo = "Psíquico",
                Rareza = "Rara",
                Estado = "Buena",
                ValorEstimado = 800,
                Imagen = "dotnet_bot.png",
                EsFavorito = true
            },

            new CartaPokemon
            {
                Nombre = "Bulbasaur",
                Categoria = "Pokémon",
                Tipo = "Planta",
                Rareza = "Común",
                Estado = "Buena",
                ValorEstimado = 100,
                Imagen = "dotnet_bot.png",
                EsFavorito = false
            }

            };
        }

        [RelayCommand]
        async Task VerDetalle(CartaPokemon carta)
        {
            if (carta is null)
                return;
            await Shell.Current.GoToAsync(nameof(DetalleCarta), true, new Dictionary<string, object>
            {
                { "Carta", carta }
            });
        }

        [RelayCommand]
        async Task IrAAgregar()
        {
            await Shell.Current.GoToAsync(nameof(CartaFormPage));
        }

        // Llamado desde CartaFormViewModel cuando se guarda una carta nueva
        public void AgregarCarta(CartaPokemon carta)
        {
            if (carta is null)
                return;

            Cartas.Add(carta);
        }
    }
}
