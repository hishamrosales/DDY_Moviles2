using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using DDY.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using static Android.Hardware.Camera;
using static Android.Icu.Text.CaseMap;

namespace DDY.ViewModels
{
    // Reutilizada para Agregar y Editar.
    // Si llega "Carta" por QueryProperty -> modo Editar (se edita el objeto por referencia).
    // Si no llega nada -> modo Agregar (se crea una carta nueva y se agrega al ListaViewModel).
    [QueryProperty(nameof(CartaOriginal), "Carta")]
    public partial class CartaFormViewModel : ObservableObject
    {
        private readonly ListaViewModel listaViewModel;

        [ObservableProperty]
        private CartaPokemon? cartaOriginal;

        [ObservableProperty]
        private string titulo = "Agregar carta";

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
        private string valorEstimadoTexto = string.Empty;

        [ObservableProperty]
        private string imagen = "dotnet_bot.png";

        [ObservableProperty]
        private bool esFavorito;

        [ObservableProperty]
        private string mensajeError = string.Empty;

        [ObservableProperty]
        private bool tieneError;

        public List<string> Categorias { get; } = new() { "Pokémon", "Entrenador", "Energía" };
        public List<string> Estados { get; } = new() { "Nueva", "Buena", "Regular", "Dañada" };

        public CartaFormViewModel(ListaViewModel listaViewModel)
        {
            this.listaViewModel = listaViewModel;
        }

        // Se dispara automáticamente cuando llega la carta por QueryProperty (modo Editar)
        partial void OnCartaOriginalChanged(CartaPokemon? value)
        {
            if (value is null)
                return;

            Titulo = "Editar carta";
            Nombre = value.Nombre;
            Categoria = value.Categoria;
            Tipo = value.Tipo;
            Rareza = value.Rareza;
            Estado = value.Estado;
            ValorEstimadoTexto = value.ValorEstimado.ToString();
            Imagen = value.Imagen;
            EsFavorito = value.EsFavorito;
        }

        [RelayCommand]
        private async Task Guardar()
        {
            TieneError = false;

            if (string.IsNullOrWhiteSpace(Nombre))
            {
                MostrarError("El nombre es obligatorio.");
                return;
            }

            if (string.IsNullOrWhiteSpace(Categoria))
            {
                MostrarError("Selecciona una categoría.");
                return;
            }

            if (string.IsNullOrWhiteSpace(Estado))
            {
                MostrarError("Selecciona un estado.");
                return;
            }

            if (!decimal.TryParse(ValorEstimadoTexto, out decimal valor) || valor < 0)
            {
                MostrarError("Ingresa un valor estimado válido.");
                return;
            }

            if (CartaOriginal is not null)
            {
                // Modo Editar: mismo objeto por referencia -> se refleja solo en Lista/Favoritos
                CartaOriginal.Nombre = Nombre;
                CartaOriginal.Categoria = Categoria;
                CartaOriginal.Tipo = Tipo;
                CartaOriginal.Rareza = Rareza;
                CartaOriginal.Estado = Estado;
                CartaOriginal.ValorEstimado = valor;
                CartaOriginal.Imagen = Imagen;
                CartaOriginal.EsFavorito = EsFavorito;
            }
            else
            {
                // Modo Agregar
                var nuevaCarta = new CartaPokemon
                {
                    Nombre = Nombre,
                    Categoria = Categoria,
                    Tipo = Tipo,
                    Rareza = Rareza,
                    Estado = Estado,
                    ValorEstimado = valor,
                    Imagen = Imagen,
                    EsFavorito = EsFavorito
                };

                listaViewModel.AgregarCarta(nuevaCarta);
            }

            await Shell.Current.GoToAsync("..");
        }

        [RelayCommand]
        private async Task Cancelar()
        {
            await Shell.Current.GoToAsync("..");
        }

        private void MostrarError(string mensaje)
        {
            MensajeError = mensaje;
            TieneError = true;
        }
    }
}