namespace backend.DTOs;
using backend.modelos;

public class ActualizarTurnoDTO
{
    public DateTime FechaHoraInicio { get; set; }

    public int? IdProfesional { get; set; }

    public EstadoTurno Estado { get; set; }

    public string Observaciones { get; set; } = string.Empty;
}