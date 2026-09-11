using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using DDY.DATAS.DTOs;
using DDY.Models;

namespace DDY.Services
{
    public partial class CartaApiService : ObservableObject
    {
        private readonly HttpClient _http;
        public ObservableCollection<CartaPokemon> Cartas { get; } = new();

        [ObservableProperty]
        private bool estaCargando;

        [ObservableProperty]
        private bool tieneError;

        [ObservableProperty]
        private string mensajeError = string.Empty;

        public CartaApiService()
        {
            _http = new HttpClient
            {
                Timeout = TimeSpan.FromSeconds(8)
            };
        }

        public async Task CargarCartasInicialesAsync(string url = "https://api.pokemontcg.io/v2/cards?pageSize=20", bool forzarRecarga = false)
        {
            if (!forzarRecarga && Cartas.Count > 0) return;

            MainThread.BeginInvokeOnMainThread(() =>
            {
                TieneError = false;
                MensajeError = string.Empty;
                EstaCargando = true;
            });

            await Task.Delay(500);

            try
            {
                var respuesta = await _http.GetFromJsonAsync<CartaApiResponse>(url);

                if (respuesta?.Data != null)
                {
                    MainThread.BeginInvokeOnMainThread(() =>
                    {
                        // Preserve las cartas agregadas manualmente por el usuario
                        var cartasLocales = Cartas.Where(c => c.EsLocal).ToList();

                        Cartas.Clear();

                        // 1. Re-insertar cartas locales
                        foreach (var local in cartasLocales)
                        {
                            Cartas.Add(local);
                        }

                        // 2. Insertar cartas de la API
                        foreach (var item in respuesta.Data)
                        {
                            Cartas.Add(new CartaPokemon
                            {
                                Id = item.Id ?? Guid.NewGuid().ToString(),
                                Nombre = item.Name ?? "Sin nombre",
                                Categoria = "Pokémon",
                                Tipo = item.Types != null ? string.Join(", ", item.Types) : "Normal",
                                Rareza = "Rara",
                                Estado = "Buena",
                                ValorEstimado = 150.00m,
                                Imagen = item.Images?.Small ?? "dotnet_bot.png",
                                EsFavorito = false,
                                EsLocal = false
                            });
                        }
                    });
                }
            }
            catch (TaskCanceledException)
            {
                TieneError = true;
                MensajeError = "La petición tardó demasiado. Verifica tu conexión e intenta de nuevo.";
            }
            catch (HttpRequestException ex)
            {
                TieneError = true;
                MensajeError = $"No se pudo conectar al servidor ({ex.StatusCode}).";
            }
            catch (JsonException)
            {
                TieneError = true;
                MensajeError = "La respuesta del servidor no se pudo interpretar.";
            }
            catch (Exception ex)
            {
                TieneError = true;
                MensajeError = $"Error de conexión: {ex.Message}";
            }
            finally
            {
                EstaCargando = false;
            }
        }

        public ObservableCollection<CartaPokemon> ObtenerTodos() => Cartas;

        public CartaPokemon? ObtenerPorId(string id)
        {
            return Cartas.FirstOrDefault(c => c.Id == id);
        }

        public void Agregar(CartaPokemon carta)
        {
            if (string.IsNullOrEmpty(carta.Id))
                carta.Id = Guid.NewGuid().ToString();

            Cartas.Add(carta);
        }

        public void Actualizar(CartaPokemon carta)
        {
            var existente = ObtenerPorId(carta.Id);
            if (existente != null)
            {
                existente.Nombre = carta.Nombre;
                existente.Categoria = carta.Categoria;
                existente.Tipo = carta.Tipo;
                existente.Rareza = carta.Rareza;
                existente.Estado = carta.Estado;
                existente.ValorEstimado = carta.ValorEstimado;
                existente.Imagen = carta.Imagen;
                existente.EsFavorito = carta.EsFavorito;
            }
        }

        public void Eliminar(CartaPokemon carta)
        {
            if (carta != null && Cartas.Contains(carta))
            {
                Cartas.Remove(carta);
            }
        }
    }
}