using backend.Datos;
using backend.DTOs;
using backend.modelos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ServicioController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ServicioController(AppDbContext context)
        {
            _context = context;
        }

        // ENDPOINTS
        // Metodo GET
        [HttpGet]
        public async Task<IActionResult> GetServicios()
        {
            var servicios = await _context.Servicios.Select(s => new ServicioRespuestaDTO
            {
                Id = s.Id,
                Nombre = s.Nombre,
                DuracionMinutos = s.DuracionMinutos,
                Costo = s.Costo,
                Estado = s.Estado,
                IdCategoria = s.IdCategoria,
                IdNegocio = s.IdNegocio
            }).ToListAsync();

            return Ok(servicios);
        }

        // Metodo GET por ID
        [HttpGet("{id}")]
        public async Task<IActionResult> GetServicioPorId(int id)
        {
            var servicio = await _context.Servicios
                .Where(s => s.Id == id)
                .Select(s => new ServicioRespuestaDTO
                {
                    Id = s.Id,
                    Nombre = s.Nombre,
                    DuracionMinutos = s.DuracionMinutos,
                    Costo = s.Costo,
                    Estado = s.Estado,
                    IdCategoria = s.IdCategoria,
                    IdNegocio = s.IdNegocio
                })
                .FirstOrDefaultAsync();

            if (servicio == null)
            {
                return NotFound(new
                {
                    mensaje = $"No existe un servicio con ID {id}."
                });
            }

            return Ok(servicio);
        }

        // Metodo POST
        [HttpPost]
        public async Task<IActionResult> CrearServicio(ServicioDTO servicioDTO)
        {
            // Validar nombre
            if (string.IsNullOrWhiteSpace(servicioDTO.Nombre))
            {
                return BadRequest(new
                {
                    mensaje = "El nombre del servicio es obligatorio."
                });
            }

            // Validar duración
            if (servicioDTO.DuracionMinutos <= 0)
            {
                return BadRequest(new
                {
                    mensaje = "La duración debe ser mayor."
                });
            }

            // Validar costo
            if (servicioDTO.Costo < 0)
            {
                return BadRequest(new
                {
                    mensaje = "El costo no puede ser negativo."
                });
            }

            // Verificar que exista el negocio
            bool existeNegocio = await _context.Negocios
                .AnyAsync(n => n.Id == servicioDTO.IdNegocio);  // AnyAsync() solo verifica existencia, no devuelve el obj.

            if (!existeNegocio)
            {
                return BadRequest(new
                {
                    mensaje = "El negocio indicado no existe."
                });
            }

            // Si tiene categoría, verificar que exista
            if (servicioDTO.IdCategoria.HasValue)
            {
                bool existeCategoria = await _context.Categorias
                    .AnyAsync(c => c.Id == servicioDTO.IdCategoria.Value);

                if (!existeCategoria)
                {
                    return BadRequest(new
                    {
                        mensaje = "La categoría indicada no existe."
                    });
                }
            }

            // Crear la entidad
            var servicio = new Servicio
            {
                Nombre = servicioDTO.Nombre,
                DuracionMinutos = servicioDTO.DuracionMinutos,
                Costo = servicioDTO.Costo,
                Estado = servicioDTO.Estado,
                IdCategoria = servicioDTO.IdCategoria,
                IdNegocio = servicioDTO.IdNegocio
            };

            _context.Servicios.Add(servicio);

            await _context.SaveChangesAsync();

            return Ok(new
            {
                mensaje = "Servicio creado correctamente.",
                id = servicio.Id
            });
        }

        // Metodo PUT
        [HttpPut("{id}")]
        public async Task<IActionResult> ActualizarServicio(int id, ServicioDTO servicioDTO)
        {
            // Buscar el servicio
            var servicio = await _context.Servicios.FindAsync(id);

            if (servicio == null)
            {
                return NotFound(new
                {
                    mensaje = $"No existe un servicio con ID {id}."
                });
            }

            // Validar nombre
            if (string.IsNullOrWhiteSpace(servicioDTO.Nombre))
            {
                return BadRequest(new
                {
                    mensaje = "El nombre del servicio es obligatorio."
                });
            }

            // Validar duración
            if (servicioDTO.DuracionMinutos <= 0)
            {
                return BadRequest(new
                {
                    mensaje = "La duración debe ser mayor que cero."
                });
            }

            // Validar costo
            if (servicioDTO.Costo < 0)
            {
                return BadRequest(new
                {
                    mensaje = "El costo no puede ser negativo."
                });
            }

            // Verificar negocio
            bool existeNegocio = await _context.Negocios
                .AnyAsync(n => n.Id == servicioDTO.IdNegocio);

            if (!existeNegocio)
            {
                return BadRequest(new
                {
                    mensaje = "El negocio indicado no existe."
                });
            }

            // Verificar categoría
            if (servicioDTO.IdCategoria.HasValue)
            {
                bool existeCategoria = await _context.Categorias
                    .AnyAsync(c => c.Id == servicioDTO.IdCategoria.Value);

                if (!existeCategoria)
                {
                    return BadRequest(new
                    {
                        mensaje = "La categoría indicada no existe."
                    });
                }
            }

            // Actualizar entidad
            servicio.Nombre = servicioDTO.Nombre;
            servicio.DuracionMinutos = servicioDTO.DuracionMinutos;
            servicio.Costo = servicioDTO.Costo;
            servicio.Estado = servicioDTO.Estado;
            servicio.IdCategoria = servicioDTO.IdCategoria;
            servicio.IdNegocio = servicioDTO.IdNegocio;

            await _context.SaveChangesAsync();

            return Ok(new
            {
                mensaje = "Servicio actualizado correctamente."
            });
        }

        // Metodo DELETE
        [HttpDelete("{id}")]
        public async Task<IActionResult> EliminarServicio(int id)
        {
            var servicio = await _context.Servicios.FindAsync(id);

            if (servicio == null)
            {
                return NotFound(new
                {
                    mensaje = $"No existe un servicio con ID {id}."
                });
            }

            _context.Servicios.Remove(servicio);

            await _context.SaveChangesAsync();

            return Ok(new
            {
                mensaje = "Servicio eliminado correctamente."
            });
        }
    }
}