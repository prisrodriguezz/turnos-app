namespace backend.modelos;

public class Categoria
{
    public int Id {get; set;}
    public string Descripcion {get; set;} = string.Empty;
    public bool Estado {get; set;}

    // Una categoria puede tener muchos servicios
    public ICollection<Servicio> Servicios { get; set; } = [];
}