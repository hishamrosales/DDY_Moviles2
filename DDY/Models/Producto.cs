using System;
using System.Collections.Generic;
using System.Text;

namespace DDY.Models
{
    public class Producto
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;

        public string Descripcion { get; set; } = string.Empty;

        public decimal Precio { get; set; }

        public string Categoria { get; set; } = string.Empty;

        public string Imagen { get; set; } = string.Empty;

    }
}
