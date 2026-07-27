using backend.Datos;
using backend.DTOs;
using backend.modelos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProfesionalServicioController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ProfesionalServicioController(AppDbContext context)
        {
            _context = context;
        }

        // Metodo GET - Ver servicios de un profesional
        [HttpGet("profesional/{idProfesional}")]  // api/profesionalservicio/profesional/1
        public async Task<IActionResult> ObtenerServiciosProfesional(int idProfesional)
        {
            var servicios = await _context.ProfesionalesServicios
                .Where(ps => ps.IdProfesional == idProfesional)
                .Select(ps => new
                {
                    ps.Servicio.Id,
                    ps.Servicio.Nombre,
                    ps.Servicio.DuracionMinutos,
                    ps.Servicio.Costo
                })
                .ToListAsync();

            return Ok(servicios);
        }

        // Metodo POST - Asignar servicio a un profesional
        [HttpPost]
        public async Task<IActionResult> AsignarServicio(ProfesionalServicioDTO dto)
        {
            // Verificar profesional
            var profesionalExiste = await _context.Profesionales
                .AnyAsync(p => p.Id == dto.IdProfesional);

            if (!profesionalExiste)
            {
                return BadRequest(new
                {
                    mensaje = "El profesional no existe."
                });
            }

            // Verificar servicio
            var servicioExiste = await _context.Servicios
                .AnyAsync(s => s.Id == dto.IdServicio);

            if (!servicioExiste)
            {
                return BadRequest(new
                {
                    mensaje = "El servicio no existe."
                });
            }

            // Evitar duplicados
            var existeRelacion = await _context.ProfesionalesServicios
                .AnyAsync(ps =>
                    ps.IdProfesional == dto.IdProfesional &&
                    ps.IdServicio == dto.IdServicio);

            if (existeRelacion)
            {
                return BadRequest(new
                {
                    mensaje = "El profesional ya tiene asignado ese servicio."
                });
            }

            // Crear relacion
            var profesionalServicio = new ProfesionalServicio
            {
                IdProfesional = dto.IdProfesional,
                IdServicio = dto.IdServicio
            };

            _context.ProfesionalesServicios.Add(profesionalServicio);

            await _context.SaveChangesAsync();

            return Ok(new
            {
                mensaje = "Servicio asignado correctamente."
            });

        }

        // Metodo DELETE - Quitar servicio a un profesional
        [HttpDelete("{idProfesional}/{idServicio}")]    // api/profesionalservicio/1/1
        public async Task<IActionResult> QuitarServicio(int idProfesional, int idServicio)
        {
            var relacion = await _context.ProfesionalesServicios
                .FirstOrDefaultAsync(ps => ps.IdProfesional == idProfesional && ps.IdServicio == idServicio);

            if(relacion == null)
            {
                return NotFound(new
                {
                    mensaje = "La asignación no existe."
                });
            }

            _context.ProfesionalesServicios.Remove(relacion);

            await _context.SaveChangesAsync();

            return Ok(new
            {
                mensaje = "Servicio eliminado del profesional."
            });
        }
    }
}