namespace backend.DTOs;

public class DisponibilidadProfesionalDTO
{
    public DayOfWeek DiaSemana { get; set; }

    public TimeOnly HoraInicio { get; set; }

    public TimeOnly HoraFin { get; set; }

    public int IdProfesional { get; set; }
}