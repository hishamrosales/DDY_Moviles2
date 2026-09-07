using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;
using DDY.Models;
using DDY.DATAS.DTOs;

namespace DDY.Services
{
    public class CartaApiService
    {
        private readonly HttpClient _http;

        public CartaApiService()
        {
            _http = new HttpClient
            {
                Timeout = TimeSpan.FromSeconds(8)
            };
        }

        public async Task<(List<CartasApi> cartas, string error)> ObtenerCartasAsync(
            string url = "https://api.pokemontcg.io/v2/cards?pageSize=20")
        {
            try
            {
                var respuesta = await _http.GetFromJsonAsync<CartaApiResponse>(url);

                var cartas = respuesta?.Data?.Select(d => new CartasApi 
                {
                    Id = d.Id,
                    Nombre = d.Name,
                    ImagenUrl = d.Images?.Small
                }).ToList() ?? new List<CartasApi>();

                return (cartas, null);
            }
            catch (TaskCanceledException)
            {
                return (
                    new List<CartasApi>(),
                    "La petición tardó demasiado. Verifica tu conexión e intenta de nuevo."
                );
            }
            catch (HttpRequestException ex)
            {
                return (
                    new List<CartasApi>(),
                    $"No se pudo conectar al servidor ({ex.StatusCode})."
                );
            }
            catch (JsonException)
            {
                return (
                    new List<CartasApi>(),
                    "La respuesta del servidor no se pudo interpretar."
                );
            }
        }

        public async Task<(CartasApi carta, string error)> ObtenerCartaPorIdAsync(
            string id,
            string urlBase = "https://api.pokemontcg.io/v2/cards")
        {
            try
            {
                var url = $"{urlBase}/{id}";

                var respuesta =
                    await _http.GetFromJsonAsync<CartaApiSingleResponse>(url);

                if (respuesta?.Data is null)
                {
                    return (null, "No se encontró la carta.");
                }

                var dto = respuesta.Data;

                var carta = new CartasApi
                {
                    Id = dto.Id,
                    Nombre = dto.Name,
                    Descripcion = dto.FlavorText,
                    ImagenUrl = dto.Images?.Large,
                    Tipo = dto.Types != null
                        ? string.Join(", ", dto.Types)
                        : ""
                };

                return (carta, null);
            }
            catch (TaskCanceledException)
            {
                return (
                    null,
                    "La petición tardó demasiado. Verifica tu conexión e intenta de nuevo."
                );
            }
            catch (HttpRequestException ex)
            {
                return (
                    null,
                    $"No se pudo conectar al servidor ({ex.StatusCode})."
                );
            }
            catch (JsonException)
            {
                return (
                    null,
                    "La respuesta del servidor no se pudo interpretar."
                );
            }
        }
    }


    public class CartaApiResponse
    {
        public List<CartaApiDto> Data { get; set; }
    }


    public class CartaApiSingleResponse
    {
        public CartaApiDto Data { get; set; }
    }

  
    public class CartaApiDto
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string FlavorText { get; set; }

        public List<string> Types { get; set; }

        public CartaImages Images { get; set; }
    }


    public class CartaImages
    {
        public string Small { get; set; }

        public string Large { get; set; }
    }
}