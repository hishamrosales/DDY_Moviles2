using System;
using System.Collections.Generic;
using System.Text;

namespace DDY.DATAS.DTOs
{
    public class CartasApi
    {
        public string Id { get; set; } = string.Empty;
        public string Nombre { get; set; } = string.Empty;
        public string Descripcion { get; set; } = string.Empty;
        public string ImagenUrl { get; set; } = string.Empty;
        public string Tipo { get; set; } = string.Empty;
    }

    public class CartaApiResponse
    {
        public List<CartaApiDto>? Data { get; set; }
    }

    public class CartaApiSingleResponse
    {
        public CartaApiDto? Data { get; set; }
    }

    public class CartaApiDto
    {
        public string Id { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string FlavorText { get; set; } = string.Empty;
        public List<string>? Types { get; set; }
        public CartaImages? Images { get; set; }
    }

    public class CartaImages
    {
        public string? Small { get; set; }
        public string? Large { get; set; }
    }
}
