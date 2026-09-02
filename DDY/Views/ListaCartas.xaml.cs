using CatalogoPokemon.Models;

namespace DDY.Views;

public partial class ListaCartas : ContentPage
{
    List<CartaPokemon> cartas = new List<CartaPokemon>
        {
            new CartaPokemon
            {
                Nombre = "Pikachu",
                Categoria = "Pokémon",
                Tipo = "Eléctrico",
                Rareza = "Rara",
                Estado = "Buena",
                ValorEstimado = 250,
                Imagen = "pikachu.png",
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
                Imagen = "charizard.png",
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
                Imagen = "mew.png",
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
                Imagen = "bulbasaur.png",
                EsFavorito = false
            }
        };

    public ListaCartas()
    {
        InitializeComponent();

        BindingContext = cartas;
    }
}