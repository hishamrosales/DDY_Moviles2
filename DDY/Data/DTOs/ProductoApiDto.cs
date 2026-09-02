using System;
using System.Collections.Generic;
using System.Text;

namespace DDY.Data.DTOs
{
    // This class represents a data transfer object (DTO) for a Pokémon card retrieved from an API. It contains the information needed to display and manage Pokémon cards in the catalog.

    public class ProductoApiDto
    {
        public int Id { get; set; }

        public string Title { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public decimal Price { get; set; } 

        public string Category { get; set; } = string.Empty;

        public string ImageUrl { get; set; } = string.Empty;

    }
}
