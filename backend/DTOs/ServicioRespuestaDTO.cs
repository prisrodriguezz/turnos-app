namespace backend.DTOs;

public class ServicioRespuestaDTO
{
    public int Id { get; set; }

    public string Nombre { get; set; } = string.Empty;
    public int DuracionMinutos { get; set; }
    public decimal Costo { get; set; }
    public bool Estado { get; set; }

    public int? IdCategoria { get; set; }
    public int IdNegocio { get; set; }
}