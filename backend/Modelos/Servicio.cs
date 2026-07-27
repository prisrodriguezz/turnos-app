namespace backend.modelos;

public class Servicio
{
    public int Id {get; set;}
    public string Nombre {get; set;} = string.Empty;
    public int DuracionMinutos {get; set;}
    public decimal Costo {get; set;}
    public bool Estado {get; set;}

    public int? IdCategoria {get; set;}
    public int IdNegocio {get; set;}
    public Categoria? Categoria {get; set;} = null!;
    public Negocio Negocio {get; set;} = null!;

    public ICollection<Turno> Turnos { get; set; } = [];
    public ICollection<ProfesionalServicio> ProfesionalServicios { get; set; } = [];
}