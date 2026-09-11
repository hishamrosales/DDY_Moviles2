using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using DDY.Models;
using DDY.Services;
using Microsoft.Maui.Controls;

namespace DDY.ViewModels
{
    [QueryProperty(nameof(CartaParaEditar), "Carta")]
    [QueryProperty(nameof(CartaId), "id")]
    public partial class CartaFormViewModels : ObservableObject, IQueryAttributable
    {
        private readonly CartaApiService _apiService;

        [ObservableProperty]
        private CartaPokemon? cartaParaEditar;

        [ObservableProperty]
        private string cartaId = string.Empty;

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

        public CartaFormViewModels(CartaApiService apiService)
        {
            _apiService = apiService;
        }

        public void ApplyQueryAttributes(IDictionary<string, object> query)
        {
            if (query != null && query.TryGetValue("Carta", out var paramObj) && paramObj is CartaPokemon carta)
            {
                CargarDatosEnFormulario(carta);
            }
            else if (query != null && query.TryGetValue("id", out var paramId) && paramId != null)
            {
                var cartaEncontrada = _apiService.ObtenerPorId(paramId.ToString()!);
                if (cartaEncontrada != null)
                {
                    CargarDatosEnFormulario(cartaEncontrada);
                }
            }
            else
            {
                // Si la navegación no trae parámetros (Modo "Agregar"), se resetea todo el formulario
                LimpiarFormulario();
            }
        }

        private void CargarDatosEnFormulario(CartaPokemon carta)
        {
            CartaParaEditar = carta;
            CartaId = carta.Id;
            Titulo = "Editar carta";
            Nombre = carta.Nombre;
            Categoria = carta.Categoria;
            Tipo = carta.Tipo;
            Rareza = carta.Rareza;
            Estado = carta.Estado;
            ValorEstimadoTexto = carta.ValorEstimado.ToString();
            Imagen = carta.Imagen;
            EsFavorito = carta.EsFavorito;
        }

        private void LimpiarFormulario()
        {
            CartaParaEditar = null;
            CartaId = string.Empty;
            Titulo = "Agregar carta";
            Nombre = string.Empty;
            Categoria = string.Empty;
            Tipo = string.Empty;
            Rareza = string.Empty;
            Estado = string.Empty;
            ValorEstimadoTexto = string.Empty;
            Imagen = "dotnet_bot.png";
            EsFavorito = false;
            TieneError = false;
            MensajeError = string.Empty;
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

            if (string.IsNullOrEmpty(CartaId))
            {
                // Crear nueva carta
                var nuevaCarta = new CartaPokemon
                {
                    Id = Guid.NewGuid().ToString(),
                    Nombre = Nombre,
                    Categoria = Categoria,
                    Tipo = Tipo,
                    Rareza = Rareza,
                    Estado = Estado,
                    ValorEstimado = valor,
                    Imagen = Imagen,
                    EsFavorito = EsFavorito,
                    EsLocal = true
                };
                _apiService.Agregar(nuevaCarta);
            }
            else
            {
                // Actualizar carta existente
                var cartaExistente = _apiService.ObtenerPorId(CartaId);
                if (cartaExistente != null)
                {
                    cartaExistente.Nombre = Nombre;
                    cartaExistente.Categoria = Categoria;
                    cartaExistente.Tipo = Tipo;
                    cartaExistente.Rareza = Rareza;
                    cartaExistente.Estado = Estado;
                    cartaExistente.ValorEstimado = valor;
                    cartaExistente.Imagen = Imagen;
                    cartaExistente.EsFavorito = EsFavorito;
                    _apiService.Actualizar(cartaExistente);
                }
            }

            LimpiarFormulario();
            await Shell.Current.GoToAsync("..");
        }

        [RelayCommand]
        private async Task Cancelar()
        {
            LimpiarFormulario();
            await Shell.Current.GoToAsync("..");
        }

        private void MostrarError(string mensaje)
        {
            MensajeError = mensaje;
            TieneError = true;
        }
    }
}