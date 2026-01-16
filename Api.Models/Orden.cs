public class Orden
{
    public int IdOrden { get; set; }
    public int IdCliente { get; set; }
    public string Cliente { get; set; }
    public decimal TotalOrden { get; set; }
    public DateTime Fecha { get; set; }
    public List<DetalleOrden> Detalles { get; set; }
}
