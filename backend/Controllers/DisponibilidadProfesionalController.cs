using backend.Datos;
using backend.DTOs;
using backend.modelos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DisponibilidadProfesionalController : ControllerBase
    {
        private readonly AppDbContext _context;

        public DisponibilidadProfesionalController(AppDbContext context)
        {
            _context = context;
        }

        // Metodo GET - Ver disponibilidad de un profesional
        [HttpGet("profesional/{idProfesional}")]    // api/disponibilidadprofesional/profesional/1
        public async Task<IActionResult> ObtenerDisponibilidad(int idProfesional)
        {
            var disponibilidad = await _context.DisponibilidadesProfesional
                .Where(d => d.IdProfesional == idProfesional).Select(d => new
                {
                    d.Id,
                    d.DiaSemana,
                    d.HoraInicio,
                    d.HoraFin
                }).ToListAsync();

            return Ok(disponibilidad);
        }

        // Metodo POST - Crear disponibilidad
        [HttpPost]
        public async Task<IActionResult> CrearDisponibilidad(DisponibilidadProfesionalDTO dto)
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

            // Validar horario
            if (dto.HoraInicio >= dto.HoraFin)
            {
                return BadRequest(new
                {
                    mensaje = "La hora de inicio debe ser menor a la hora final."
                });
            }

            // Evitar horarios duplicados
            bool existe = await _context.DisponibilidadesProfesional
                .AnyAsync(d =>
                    d.IdProfesional == dto.IdProfesional &&
                    d.DiaSemana == dto.DiaSemana &&
                    d.HoraInicio == dto.HoraInicio &&
                    d.HoraFin == dto.HoraFin);

            if (existe)
            {
                return BadRequest(new
                {
                    mensaje = "La disponibilidad ya existe."
                });
            }

            // Crear disponibilidad
            var disponibilidad = new DisponibilidadProfesional
            {
                DiaSemana = dto.DiaSemana,
                HoraInicio = dto.HoraInicio,
                HoraFin = dto.HoraFin,
                IdProfesional = dto.IdProfesional
            };

            _context.DisponibilidadesProfesional.Add(disponibilidad);

            await _context.SaveChangesAsync();

            return Ok(new
            {
                mensaje = "Disponibilidad creada correctamente.",
                id = disponibilidad.Id
            });
        }

        // Metodo PUT
        [HttpPut("{id}")]
        public async Task<IActionResult> ActualizarDisponibilidad(int id, DisponibilidadProfesionalDTO dto)
        {
            var disponibilidad = await _context.DisponibilidadesProfesional.FindAsync(id);

            if(disponibilidad == null)
            {
                return NotFound(new
                {
                    mensaje = "La disponibilidad no existe."
                });
            }

            if(dto.HoraInicio >= dto.HoraFin)
            {
                return BadRequest(new
                {
                    mensaje = "Horario inválido."
                });
            }

            disponibilidad.DiaSemana = dto.DiaSemana;
            disponibilidad.HoraInicio = dto.HoraInicio;
            disponibilidad.HoraFin = dto.HoraFin;

            await _context.SaveChangesAsync();

            return Ok(new
            {
                mensaje = "Disponibilidad actualizada correctamente."
            });
        }

        // Metodo DELETE
        [HttpDelete("{id}")]
        public async Task<IActionResult> EliminarDisponibilidad(int id)
        {
            var disponibilidad = await _context.DisponibilidadesProfesional.FindAsync(id);

            if(disponibilidad == null)
            {
                return NotFound(new
                {
                    mensaje = "La disponibilidad no existe."
                });
            }

            _context.DisponibilidadesProfesional.Remove(disponibilidad);

            await _context.SaveChangesAsync();

            return Ok(new
            {
                mensaje = "Disponibilidad eliminada correctamente."
            });
        }
    }
}