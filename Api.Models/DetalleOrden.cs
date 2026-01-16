public class DetalleOrden
{
    public int OrdenId;

    public int IdDetalleOrden { get; set; }
    public int IdProducto { get; set; }
    public string Producto { get; set; }
    public int Cantidad { get; set; }
    public decimal Precio { get; set; }
    public decimal Subtotal { get; set; }
}
