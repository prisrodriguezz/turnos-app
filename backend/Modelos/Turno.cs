namespace backend.modelos;

public class Turno
{
    public int Id {get; set;}
    public DateTime FechaHoraInicio {get; set;}
    public DateTime FechaHoraFin {get; set;}
    public EstadoTurno Estado {get; set;}
    public decimal Total {get; set;}
    public string Observaciones {get; set;} = string.Empty;
    public int IdCliente {get; set;}
    public int IdServicio {get; set;}
    public int? IdProfesional {get; set;}

    public Cliente Cliente {get; set;} = null!;
    public Servicio Servicio {get; set;} = null!;
    public Profesional? Profesional {get; set;}
}