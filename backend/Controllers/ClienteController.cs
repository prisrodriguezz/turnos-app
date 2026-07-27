using backend.Datos;
using backend.DTOs;
using backend.modelos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClienteController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ClienteController(AppDbContext context)
        {
            _context = context;
        }

        // ENDPOINT
        // Metodo GET
        [HttpGet]
        public async Task<IActionResult> GetClientes()
        {
            var clientes = await _context.Clientes.Select(c => new ClienteRespuestaDTO
            {
                Id = c.Id,
                Nombre = c.Nombre,
                Apellido = c.Apellido,
                Telefono = c.Telefono,
                Email = c.Email,
                FechaRegistro = c.FechaRegistro
            }).ToListAsync();

            return Ok(clientes);
        }

        // Metodo GET por ID
        [HttpGet("{id}")]
        public async Task<IActionResult> GetClientePorId(int id)
        {
            var cliente = await _context.Clientes.Where(c => c.Id == id).Select(c => new ClienteRespuestaDTO
            {
                Id = c.Id,
                Nombre = c.Nombre,
                Apellido = c.Apellido,
                Telefono = c.Telefono,
                Email = c.Email,
                FechaRegistro = c.FechaRegistro
            }).FirstOrDefaultAsync();

            if (cliente == null)
            {
                return NotFound(new
                {
                    mensaje = $"No existe un cliente con ID {id}."
                });
            }

            return Ok(cliente);
        }

        // Metodo POST
        [HttpPost]
        public async Task<IActionResult> CrearCliente(ClienteDTO clienteDTO)
        {
            // Nombre
            if (string.IsNullOrWhiteSpace(clienteDTO.Nombre))
            {
                return BadRequest(new
                {
                    mensaje = "El nombre es obligatorio."
                });
            }

            // Apellido
            if (string.IsNullOrWhiteSpace(clienteDTO.Apellido))
            {
                return BadRequest(new
                {
                    mensaje = "El apellido es obligatorio."
                });
            }

            // Teléfono
            if (string.IsNullOrWhiteSpace(clienteDTO.Telefono))
            {
                return BadRequest(new
                {
                    mensaje = "El teléfono es obligatorio."
                });
            }

            // Email
            if (string.IsNullOrWhiteSpace(clienteDTO.Email))
            {
                return BadRequest(new
                {
                    mensaje = "El email es obligatorio."
                });
            }

            try
            {
                var mail = new System.Net.Mail.MailAddress(clienteDTO.Email);
            }
            catch
            {
                return BadRequest(new
                {
                    mensaje = "El email no tiene un formato válido."
                });
            }

            // Crear Entidad
            var cliente = new Cliente
            {
                Nombre = clienteDTO.Nombre,
                Apellido = clienteDTO.Apellido,
                Telefono = clienteDTO.Telefono,
                Email = clienteDTO.Email,
                FechaRegistro = DateTime.Now
            };

            _context.Clientes.Add(cliente);

            await _context.SaveChangesAsync();

            return Ok(new
            {
                mensaje = "Cliente creado correctamente.",
                id = cliente.Id
            });
        }


        // Metodo PUT
        [HttpPut("{id}")]
        public async Task<IActionResult> ActualizarCliente(int id, ClienteDTO clienteDTO)
        {
            // Buscar cliente
            var cliente = await _context.Clientes.FindAsync(id);

            if (cliente == null)
            {
                return NotFound(new
                {
                    mensaje = $"No existe un cliente con ID {id}."
                });
            }

            // Validar nombre
            if (string.IsNullOrWhiteSpace(clienteDTO.Nombre))
            {
                return BadRequest(new
                {
                    mensaje = "El nombre es obligatorio."
                });
            }

            // Validar apellido
            if (string.IsNullOrWhiteSpace(clienteDTO.Apellido))
            {
                return BadRequest(new
                {
                    mensaje = "El apellido es obligatorio."
                });
            }

            // Validar teléfono
            if (string.IsNullOrWhiteSpace(clienteDTO.Telefono))
            {
                return BadRequest(new
                {
                    mensaje = "El teléfono es obligatorio."
                });
            }

            // Validar email
            if (string.IsNullOrWhiteSpace(clienteDTO.Email))
            {
                return BadRequest(new
                {
                    mensaje = "El email es obligatorio."
                });
            }

            // Validar formato del email
            try
            {
                var email = new System.Net.Mail.MailAddress(clienteDTO.Email);
            }
            catch
            {
                return BadRequest(new
                {
                    mensaje = "El email no tiene un formato válido."
                });
            }

            // Actualizar datos
            cliente.Nombre = clienteDTO.Nombre;
            cliente.Apellido = clienteDTO.Apellido;
            cliente.Telefono = clienteDTO.Telefono;
            cliente.Email = clienteDTO.Email;

            await _context.SaveChangesAsync();

            return Ok(new
            {
                mensaje = "Cliente actualizado correctamente."
            });
        }

        // Metodo DELETE
        [HttpDelete("{id}")]
        public async Task<IActionResult> EliminarCliente(int id)
        {
            var cliente = await _context.Clientes.FindAsync(id);

            if (cliente == null)
            {
                return NotFound(new
                {
                    mensaje = $"No existe un cliente con ID {id}."
                });
            }

            _context.Clientes.Remove(cliente);

            await _context.SaveChangesAsync();

            return Ok(new
            {
                mensaje = "Cliente eliminado correctamente."
            });
        }
    }
}