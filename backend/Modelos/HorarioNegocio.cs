namespace backend.modelos;

public class HorarioNegocio
{
    public int Id {get; set;}
    public DayOfWeek DiaSemana {get; set;}
    public TimeOnly HoraInicio {get; set;}
    public TimeOnly HoraFin {get; set;}

    public int IdNegocio {get; set;}
    public Negocio Negocio {get; set;} = null!;
}