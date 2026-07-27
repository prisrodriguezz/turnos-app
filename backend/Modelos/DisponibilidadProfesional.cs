namespace backend.modelos;

public class DisponibilidadProfesional
{
    public int Id {get; set;}
    public DayOfWeek DiaSemana {get; set;}
    public TimeOnly HoraInicio {get; set;}
    public TimeOnly HoraFin {get; set;}
    public int IdProfesional {get; set;}

    // Propiedad de navegacion
    public Profesional Profesional { get; set; } = null!;
}