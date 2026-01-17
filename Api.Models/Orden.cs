public class Orden
{
    public int IdOrden { get; set; }
    public int IdCliente { get; set; }
    public string Cliente { get; set; }
    public decimal TotalOrden { get; set; }
    public DateTime Fecha { get; set; }
    public List<DetalleOrden> Detalles { get; set; }
}

public class OrdenInsertar
{
    
        public int IdCliente { get; set; }      // ID del cliente que realiza la orden
        public List<DetalleOrdenInsertar> Detalles { get; set; }  // Lista de productos y cantidades
  
}

public class DetalleOrdenInsertar
{
    public int IdProducto { get; set; }     // ID del producto
    public int Cantidad { get; set; }       // Cantidad que se desea comprar
}