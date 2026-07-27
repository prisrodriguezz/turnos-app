namespace backend.modelos;

public class Profesional
{
    public int Id {get; set;}
    public string Nombre {get; set;} = string.Empty;
    public string Apellido {get; set;} = string.Empty;
    public string Telefono {get; set;} = string.Empty;
    public string Email {get; set;} = string.Empty;
    public bool Estado {get; set;}

    public ICollection<Turno> Turnos { get; set; } = [];

    public ICollection<DisponibilidadProfesional> Disponibilidades { get; set; } = [];

    public ICollection<ProfesionalServicio> ProfesionalServicios { get; set; } = [];
}