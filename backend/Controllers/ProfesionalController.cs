using backend.Datos;
using backend.DTOs;
using backend.modelos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProfesionalController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ProfesionalController(AppDbContext context)
        {
            _context = context;
        }

        // GET
        [HttpGet]
        public async Task<IActionResult> GetProfesionales()
        {
            var profesionales = await _context.Profesionales
                .Select(p => new ProfesionalRespuestaDTO
                {
                    Id = p.Id,
                    Nombre = p.Nombre,
                    Apellido = p.Apellido,
                    Telefono = p.Telefono,
                    Email = p.Email,
                    Estado = p.Estado
                })
                .ToListAsync();

            return Ok(profesionales);
        }

        // GET ID
        [HttpGet("{id}")]
        public async Task<IActionResult> GetProfesional(int id)
        {
            var profesional = await _context.Profesionales
                .Where(p => p.Id == id)
                .Select(p => new ProfesionalRespuestaDTO
                {
                    Id = p.Id,
                    Nombre = p.Nombre,
                    Apellido = p.Apellido,
                    Telefono = p.Telefono,
                    Email = p.Email,
                    Estado = p.Estado
                })
                .FirstOrDefaultAsync();

            if (profesional == null)
                return NotFound(new { mensaje = $"No existe un profesional con ID {id}." });

            return Ok(profesional);
        }

        // POST
        [HttpPost]
        public async Task<IActionResult> CrearProfesional(ProfesionalDTO dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Nombre))
                return BadRequest(new { mensaje = "El nombre es obligatorio." });

            if (string.IsNullOrWhiteSpace(dto.Apellido))
                return BadRequest(new { mensaje = "El apellido es obligatorio." });

            if (string.IsNullOrWhiteSpace(dto.Telefono))
                return BadRequest(new { mensaje = "El teléfono es obligatorio." });

            if (string.IsNullOrWhiteSpace(dto.Email))
                return BadRequest(new { mensaje = "El email es obligatorio." });

            try
            {
                var email = new System.Net.Mail.MailAddress(dto.Email);
            }
            catch
            {
                return BadRequest(new { mensaje = "El email no es válido." });
            }

            var profesional = new Profesional
            {
                Nombre = dto.Nombre,
                Apellido = dto.Apellido,
                Telefono = dto.Telefono,
                Email = dto.Email,
                Estado = dto.Estado
            };

            _context.Profesionales.Add(profesional);

            await _context.SaveChangesAsync();

            return Ok(new
            {
                mensaje = "Profesional creado correctamente.",
                id = profesional.Id
            });
        }

        // PUT
        [HttpPut("{id}")]
        public async Task<IActionResult> ActualizarProfesional(int id, ProfesionalDTO dto)
        {
            var profesional = await _context.Profesionales.FindAsync(id);

            if (profesional == null)
                return NotFound(new { mensaje = $"No existe un profesional con ID {id}." });

            if (string.IsNullOrWhiteSpace(dto.Nombre))
                return BadRequest(new { mensaje = "El nombre es obligatorio." });

            if (string.IsNullOrWhiteSpace(dto.Apellido))
                return BadRequest(new { mensaje = "El apellido es obligatorio." });

            if (string.IsNullOrWhiteSpace(dto.Telefono))
                return BadRequest(new { mensaje = "El teléfono es obligatorio." });

            if (string.IsNullOrWhiteSpace(dto.Email))
                return BadRequest(new { mensaje = "El email es obligatorio." });

            try
            {
                var email = new System.Net.Mail.MailAddress(dto.Email);
            }
            catch
            {
                return BadRequest(new { mensaje = "El email no es válido." });
            }

            profesional.Nombre = dto.Nombre;
            profesional.Apellido = dto.Apellido;
            profesional.Telefono = dto.Telefono;
            profesional.Email = dto.Email;
            profesional.Estado = dto.Estado;

            await _context.SaveChangesAsync();

            return Ok(new
            {
                mensaje = "Profesional actualizado correctamente."
            });
        }

        // DELETE
        [HttpDelete("{id}")]
        public async Task<IActionResult> EliminarProfesional(int id)
        {
            var profesional = await _context.Profesionales.FindAsync(id);

            if (profesional == null)
                return NotFound(new { mensaje = $"No existe un profesional con ID {id}." });

            _context.Profesionales.Remove(profesional);

            await _context.SaveChangesAsync();

            return Ok(new
            {
                mensaje = "Profesional eliminado correctamente."
            });
        }
    }
}