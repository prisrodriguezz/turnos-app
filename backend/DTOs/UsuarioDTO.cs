namespace backend.DTOs;

public class UsuarioDTO
{
    public string Nombre { get; set; } = string.Empty;

    public string Apellido { get; set; } = string.Empty;

    public string Telefono { get; set; } = string.Empty;

    public string Email { get; set; } = string.Empty;

    public string NombreUsuario { get; set; } = string.Empty;

    public string Contrasenia { get; set; } = string.Empty;

    public int IdNegocio { get; set; }
}