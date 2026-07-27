using backend.Datos;
using backend.DTOs;
using backend.modelos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HorarioNegocioController : ControllerBase
    {
        private readonly AppDbContext _context;

        public HorarioNegocioController(AppDbContext context)
        {
            _context = context;
        }

        // Metodo GET
        [HttpGet]
        public async Task<IActionResult> GetHorarios()
        {
            var horarios = await _context.HorariosNegocio
                .Select(h => new
                {
                    h.Id,
                    h.DiaSemana,
                    h.HoraInicio,
                    h.HoraFin,
                    h.IdNegocio
                })
                .ToListAsync();


            return Ok(horarios);
        }

        // Metodo GET por ID
        [HttpGet("{id}")]
        public async Task<IActionResult> GetHorarioPorId(int id)
        {
            var horario = await _context.HorariosNegocio
            .Where(h => h.Id == id)
            .Select(h => new
            {
                h.Id,
                h.DiaSemana,
                h.HoraInicio,
                h.HoraFin,
                h.IdNegocio
            }).FirstOrDefaultAsync();

            if(horario == null)
            {
                return NotFound(new
                {
                    mensaje = $"No existe un horario con ID {id}."
                });
            }

            return Ok(horario);
        }

        // Metodo POST
        [HttpPost]
        public async Task<IActionResult> CrearHorario(HorarioNegocioDTO dto)
        {
            // Verificar que exista el negocio
            var negocioExiste = await _context.Negocios.AnyAsync(n => n.Id == dto.IdNegocio);

            if(!negocioExiste)
            {
                return BadRequest(new
                {
                    mensaje = "El negocio no existe."
                });
            }

            // Validar horario
            if(dto.HoraInicio >= dto.HoraFin)
            {
                return BadRequest(new
                {
                    mensaje = "La hora de inicio debe ser menor a la hora final."
                });
            }

            // Evitar duplicados
            bool existe = await _context.HorariosNegocio
                .AnyAsync(h =>
                    h.IdNegocio == dto.IdNegocio &&
                    h.DiaSemana == dto.DiaSemana &&
                    h.HoraInicio == dto.HoraInicio &&
                    h.HoraFin == dto.HoraFin
                );

            if(existe)
            {
                return BadRequest(new
                {
                    mensaje = "El horario ya existe."
                });
            }

            // Crear horario
            var horario = new HorarioNegocio
            {
                DiaSemana = dto.DiaSemana,
                HoraInicio = dto.HoraInicio,
                HoraFin = dto.HoraFin,
                IdNegocio = dto.IdNegocio
            };

            _context.HorariosNegocio.Add(horario);

            await _context.SaveChangesAsync();

            return Ok(new
            {
                mensaje = "Horario creado correctamente.",
                id = horario.Id
            });
        }

        // Metodo PUT
        [HttpPut("{id}")]
        public async Task<IActionResult> ActualizarHorario(int id, HorarioNegocioDTO dto)
        {
            var horario = await _context.HorariosNegocio.FindAsync(id);

            if(horario == null)
            {
                return NotFound(new
                {
                    mensaje = "El horario no existe."
                });
            }

            if(dto.HoraInicio >= dto.HoraFin)
            {
                return BadRequest(new
                {
                    mensaje = "Horario inválido."
                });
            }

            horario.DiaSemana = dto.DiaSemana;
            horario.HoraInicio = dto.HoraInicio;
            horario.HoraFin = dto.HoraFin;

            await _context.SaveChangesAsync();

            return Ok(new
            {
                mensaje = "Horario actualizado correctamente."
            });
        }

        // Metodo DELETE
        [HttpDelete("{id}")]
        public async Task<IActionResult> EliminarHorario(int id)
        {

            var horario = await _context.HorariosNegocio.FindAsync(id);

            if(horario == null)
            {
                return NotFound(new
                {
                    mensaje = "El horario no existe."
                });
            }

            _context.HorariosNegocio.Remove(horario);

            await _context.SaveChangesAsync();

            return Ok(new
            {
                mensaje = "Horario eliminado correctamente."
            });
        }
    }
}