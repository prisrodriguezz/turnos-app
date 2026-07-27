namespace backend.DTOs;

public class CategoriaRespuestaDTO
{
    public int Id { get; set; }

    public string Descripcion { get; set; } = string.Empty;

    public bool Estado { get; set; }
}