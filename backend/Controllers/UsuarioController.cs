using backend.Datos;
using backend.DTOs;
using backend.modelos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsuarioController : ControllerBase
    {
        private readonly AppDbContext _context;

        public UsuarioController(AppDbContext context)
        {
            _context = context;
        }

        // Metodo POST
        [HttpPost]
        public async Task<IActionResult> CrearUsuario(UsuarioDTO dto)
        {
            if (string.IsNullOrWhiteSpace(dto.NombreUsuario))
            {
                return BadRequest(new
                {
                    mensaje = "El nombre de usuario es obligatorio."
                });
            }


            if (string.IsNullOrWhiteSpace(dto.Contrasenia))
            {
                return BadRequest(new
                {
                    mensaje = "La contraseña es obligatoria."
                });
            }


            if (dto.Contrasenia.Length < 6)
            {
                return BadRequest(new
                {
                    mensaje = "La contraseña debe tener al menos 6 caracteres."
                });
            }


            bool existeUsuario = await _context.Usuarios.AnyAsync(u => u.NombreUsuario == dto.NombreUsuario);

            if (existeUsuario)
            {
                return BadRequest(new
                {
                    mensaje = "El nombre de usuario ya existe."
                });
            }

            bool existeEmail = await _context.Usuarios.AnyAsync(u => u.Email == dto.Email);

            if (existeEmail)
            {
                return BadRequest(new
                {
                    mensaje = "El email ya está registrado."
                });
            }

            // Crear Entidad
            var usuario = new Usuario
            {
                Nombre = dto.Nombre,
                Apellido = dto.Apellido,
                Telefono = dto.Telefono,
                Email = dto.Email,
                NombreUsuario = dto.NombreUsuario,

                // Guardamos hash
                Contrasenia = BCrypt.Net.BCrypt.HashPassword(dto.Contrasenia),

                IdNegocio = dto.IdNegocio
            };


            _context.Usuarios.Add(usuario);

            await _context.SaveChangesAsync();


            return Ok(new
            {
                mensaje = "Usuario creado correctamente.",
                id = usuario.Id
            });
        }
    }
}