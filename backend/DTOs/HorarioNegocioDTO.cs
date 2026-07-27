namespace backend.DTOs;

public class HorarioNegocioDTO
{
    public DayOfWeek DiaSemana { get; set; }

    public TimeOnly HoraInicio { get; set; }

    public TimeOnly HoraFin { get; set; }

    public int IdNegocio { get; set; }
}