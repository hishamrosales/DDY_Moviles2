namespace CatalogoPokemon.Models
{
    public class CartaPokemon
    {
        public string Nombre { get; set; }
        public string Categoria { get; set; }
        public string Tipo { get; set; }
        public string Rareza { get; set; }
        public string Estado { get; set; }
        public decimal ValorEstimado { get; set; }
        public string Imagen { get; set; }
        public bool EsFavorito { get; set; }
    }
}