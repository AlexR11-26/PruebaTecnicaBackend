namespace Api.Models
{
    public class Producto
    {
        public int IdProducto { get; set; }
        public int CategoriaId { get; set; }
        public string Categoria { get; set; }
        public string Nombre { get; set; } 
        public string ImagenUrl {  get; set; }
        public decimal Precio { get; set; }
        public int Stock { get; set; }
    }
}
