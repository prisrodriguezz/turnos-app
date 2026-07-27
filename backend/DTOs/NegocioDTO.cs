namespace backend.DTOs;

// Define que datos entran y salen
public class NegocioDTO
{
    public string Nombre { get; set; } = string.Empty;
    public string Telefono { get; set; } = string.Empty;
    public string Email {get; set;} = string.Empty;
    public string Direccion { get; set; } = string.Empty;
    public bool UsaProfesionales { get; set; }
    public bool UsaCategorias { get; set; }
    public bool UsaRecordatorios { get; set; }
    public bool UsaSenia { get; set; }
}