namespace backend.modelos;

public class Negocio
{
    public int Id {get; set;}
    public string Nombre {get; set;} = string.Empty;
    public string Telefono {get; set;} = string.Empty;
    public string Email {get; set;} = string.Empty;
    public string Direccion {get; set;} = string.Empty;
    public bool UsaProfesionales {get; set;}
    public bool UsaCategorias {get; set;}
    public bool UsaRecordatorios {get; set;}
    public bool UsaSenia {get; set;}

     // Un negocio tiene muchos horarios
    public ICollection<HorarioNegocio> Horarios { get; set; } = [];
    
    // Relacion 1:N con Servicio
    public ICollection<Servicio> Servicios { get; set; } = [];
    
    // Relacion 1:1 con Usuario
    public Usuario Usuario { get; set; } = null!;
    
}