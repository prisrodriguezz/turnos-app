using backend.Datos;
using backend.DTOs;
using backend.Servicios;
using backend.modelos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TurnoController : ControllerBase
    {
        private readonly AppDbContext _context;

        private readonly GeneradorHorariosService _generador;

        public TurnoController(AppDbContext context, GeneradorHorariosService generador)
        {
            _context = context;
            _generador = generador;
        }

        // Metodo GET - Obtener profesionales por servicio
        [HttpGet("profesionales/{idServicio}")]
        public async Task<IActionResult> ObtenerProfesionalesPorServicio(int idServicio)
        {
            var profesionales = await _context.ProfesionalesServicios
                .Where(ps => ps.IdServicio == idServicio)
                .Select(ps => new ProfesionalRespuestaDTO
                {
                    Id = ps.Profesional.Id,
                    Nombre = ps.Profesional.Nombre,
                    Apellido = ps.Profesional.Apellido,
                    Telefono = ps.Profesional.Telefono,
                    Email = ps.Profesional.Email,
                    Estado = ps.Profesional.Estado
                }).ToListAsync();

            if (!profesionales.Any())
            {
                return NotFound(new
                {
                    mensaje = "No hay profesionales asignados a este servicio."
                });
            }

            return Ok(profesionales);
        }

        // Metodo GET - Obtener horarios disponibles
        [HttpGet("disponibles")]
        public async Task<IActionResult> ObtenerDisponibles(DateTime fecha, int idServicio, int? idProfesional)
        {
            var horarios = await _generador.ObtenerHorariosDisponibles(
                fecha,
                idServicio,
                idProfesional
            );

            return Ok(horarios);
        }

        // Metodo GET - Listar todos los turnos
        [HttpGet]
        public async Task<IActionResult> GetTurnos()
        {
            var turnos = await _context.Turnos
                .Include(t => t.Cliente)
                .Include(t => t.Servicio)
                .Include(t => t.Profesional)
                .Select(t => new
                {
                    t.Id,
                    Cliente = t.Cliente.Nombre + " " + t.Cliente.Apellido,
                    Servicio = t.Servicio.Nombre,
                    Profesional = t.Profesional != null
                        ? t.Profesional.Nombre + " " + t.Profesional.Apellido
                        : null,
                    t.FechaHoraInicio,
                    t.FechaHoraFin,
                    Estado = t.Estado.ToString(),
                    t.Total,
                    t.Observaciones
                })
                .OrderBy(t => t.FechaHoraInicio)
                .ToListAsync();

            return Ok(turnos);
        }

        // Metodo GET por ID
        [HttpGet("{id}")]
        public async Task<IActionResult> GetTurnoPorId(int id)
        {
            var turno = await _context.Turnos
                .Include(t => t.Cliente)
                .Include(t => t.Servicio)
                .Include(t => t.Profesional)
                .Where(t => t.Id == id)
                .Select(t => new
                {
                    t.Id,

                    Cliente = new
                    {
                        t.Cliente.Nombre,
                        t.Cliente.Apellido,
                        t.Cliente.Telefono,
                        t.Cliente.Email
                    },

                    Servicio = t.Servicio.Nombre,

                    Profesional = t.Profesional == null
                        ? null
                        : new
                        {
                            t.Profesional.Nombre,
                            t.Profesional.Apellido
                        },

                    t.FechaHoraInicio,
                    t.FechaHoraFin,
                    Estado = t.Estado.ToString(),
                    t.Total,
                    t.Observaciones
                })
                .FirstOrDefaultAsync();

            if (turno == null)
            {
                return NotFound(new
                {
                    mensaje = "Turno no encontrado."
                });
            }

            return Ok(turno);
        }

        // Metodo POST
        [HttpPost]
        public async Task<IActionResult> CrearTurno(CrearTurnoDTO dto)
        {
            // Buscar servicio
            var servicio = await _context.Servicios.FirstOrDefaultAsync(s => s.Id == dto.IdServicio);

            if (servicio == null)
            {
                return BadRequest(new
                {
                    mensaje = "El servicio no existe."
                });
            }

            // Validar que el horario siga disponible
            bool ocupado = await _context.Turnos.AnyAsync(t =>
                t.FechaHoraInicio == dto.FechaHoraInicio &&
                t.IdServicio == dto.IdServicio &&
                t.IdProfesional == dto.IdProfesional &&
                t.Estado != EstadoTurno.Cancelado
            );

            if (ocupado)
            {
                return BadRequest(new
                {
                    mensaje = "El horario seleccionado ya no está disponible."
                });
            }


            // VERIFICA SI YA ES CLIENTE, O CREA UNO NUEVO
            Cliente? cliente = null;

            // Buscar por email
            if (!string.IsNullOrWhiteSpace(dto.EmailCliente))
            {
                cliente = await _context.Clientes
                    .FirstOrDefaultAsync(c => c.Email == dto.EmailCliente);
            }

            // Si no existe, buscar por teléfono
            if (cliente == null && !string.IsNullOrWhiteSpace(dto.TelefonoCliente))
            {
                cliente = await _context.Clientes
                    .FirstOrDefaultAsync(c => c.Telefono == dto.TelefonoCliente);
            }

            // Si no existe, crear uno nuevo
            if (cliente == null)
            {
                cliente = new Cliente
                {
                    Nombre = dto.NombreCliente,
                    Apellido = dto.ApellidoCliente,
                    Telefono = dto.TelefonoCliente,
                    Email = dto.EmailCliente,
                    FechaRegistro = DateTime.Now
                };

                _context.Clientes.Add(cliente);

                await _context.SaveChangesAsync();

            }
            else
            {
                cliente.Nombre = dto.NombreCliente;
                cliente.Apellido = dto.ApellidoCliente;
                cliente.Telefono = dto.TelefonoCliente;
                cliente.Email = dto.EmailCliente;

                await _context.SaveChangesAsync();
            }


            // Crear turno
            var turno = new Turno
            {
                FechaHoraInicio = dto.FechaHoraInicio,

                FechaHoraFin = dto.FechaHoraInicio.AddMinutes(servicio.DuracionMinutos),

                Estado = EstadoTurno.Reservado,

                Total = servicio.Costo,

                Observaciones = dto.Observaciones,

                IdCliente = cliente.Id,

                IdServicio = dto.IdServicio,

                IdProfesional = dto.IdProfesional
            };

            _context.Turnos.Add(turno);

            await _context.SaveChangesAsync();

            return Ok(new
            {
                mensaje = "Turno reservado correctamente.",
                idTurno = turno.Id
            });
        }

        // Metodo PUT
        [HttpPut("{id}")]
        public async Task<IActionResult> ActualizarTurno(int id, ActualizarTurnoDTO dto)
        {
            var turno = await _context.Turnos
                .Include(t => t.Servicio)
                .FirstOrDefaultAsync(t => t.Id == id);

            if (turno == null)
            {
                return NotFound(new
                {
                    mensaje = "Turno no encontrado."
                });
            }


            bool ocupado = await _context.Turnos.AnyAsync(t =>
                t.Id != id &&
                t.FechaHoraInicio == dto.FechaHoraInicio &&
                t.IdProfesional == dto.IdProfesional &&
                t.Estado != EstadoTurno.Cancelado
            );

            if (ocupado)
            {
                return BadRequest(new
                {
                    mensaje = "Ese horario ya se encuentra ocupado."
                });
            }


            turno.FechaHoraInicio = dto.FechaHoraInicio;

            turno.FechaHoraFin = dto.FechaHoraInicio
                .AddMinutes(turno.Servicio.DuracionMinutos);

            turno.IdProfesional = dto.IdProfesional;

            turno.Estado = dto.Estado;

            turno.Observaciones = dto.Observaciones;


            await _context.SaveChangesAsync();

            return Ok(new
            {
                mensaje = "Turno actualizado correctamente."
            });
        }

        // Metodo PUT - Cancelar turno
        [HttpPut("{id}/cancelar")]
        public async Task<IActionResult> CancelarTurno(int id)
        {
            var turno = await _context.Turnos
                .FirstOrDefaultAsync(t => t.Id == id);

            if (turno == null)
            {
                return NotFound(new
                {
                    mensaje = "Turno no encontrado."
                });
            }

            turno.Estado = EstadoTurno.Cancelado;

            await _context.SaveChangesAsync();

            return Ok(new
            {
                mensaje = "Turno cancelado correctamente."
            });
        }

    }
}