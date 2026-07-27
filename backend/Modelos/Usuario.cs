namespace backend.modelos;

public class Usuario
{
    public int Id {get; set;}
    public string Nombre {get; set;} = string.Empty;
    public string Apellido {get; set;} = string.Empty;
    public string Telefono {get; set;} = string.Empty;
    public string Email {get; set;} = string.Empty;
    public string NombreUsuario {get; set;} = string.Empty;
    public string Contrasenia {get; set;} = string.Empty;

    // FK
    public int IdNegocio {get; set;}
    // Navegacion
    public Negocio Negocio { get; set; } = null!;
}