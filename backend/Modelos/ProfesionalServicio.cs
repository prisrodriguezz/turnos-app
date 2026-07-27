namespace backend.modelos;

public class ProfesionalServicio
{
    public int IdProfesional {get; set;}
    public int IdServicio {get; set;}
    public Profesional Profesional {get; set;} = null!;
    public Servicio Servicio {get; set;} = null!;
}