namespace DDY.Models
{
    public class CartaPokemon
    {
        public string Nombre { get; set; } = string.Empty;
        public string Categoria { get; set; } = string.Empty;
        public string Tipo { get; set; } = string.Empty;
        public string Rareza { get; set; } = string.Empty;
        public string Estado { get; set; } = string.Empty;
        public decimal ValorEstimado { get; set; }
        public string Imagen { get; set; } = string.Empty;
        public bool EsFavorito { get; set; }
    }
}