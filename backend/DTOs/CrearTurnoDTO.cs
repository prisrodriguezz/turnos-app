public class CrearTurnoDTO
{
    public string NombreCliente { get; set; } = string.Empty;

    public string ApellidoCliente { get; set; } = string.Empty;

    public string TelefonoCliente { get; set; } = string.Empty;

    public string EmailCliente { get; set; } = string.Empty;


    public int IdServicio { get; set; }

    public int? IdProfesional { get; set; }

    public DateTime FechaHoraInicio { get; set; }

    public string Observaciones { get; set; } = string.Empty;
}