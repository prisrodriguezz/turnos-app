using backend.Datos;
using backend.DTOs;
using backend.modelos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")] // Definimos la ruta '/api/negocio'
    public class NegocioController : ControllerBase
    {
        // Guarda referencia a la BD
        private readonly AppDbContext _context;

        // Constructor
        public NegocioController(AppDbContext context)
        {
            _context = context;
        }


        // Endpoint | GET: api/negocio
        [HttpGet]
        public async Task<IActionResult> GetNegocios()
        {
            var negocios = await _context.Negocios  // Accede al DbSet que representa la tabla Negocios
                .Select(n => new NegocioRespuestaDTO
                {
                    Id = n.Id,
                    Nombre = n.Nombre,
                    Telefono = n.Telefono,
                    Email = n.Email,
                    Direccion = n.Direccion,
                    UsaProfesionales = n.UsaProfesionales,
                    UsaCategorias = n.UsaCategorias,
                    UsaRecordatorios = n.UsaRecordatorios,
                    UsaSenia = n.UsaSenia
                })
                .ToListAsync();

                return Ok(negocios); // Devuelve lo que obtiene desde el DTO
        }

        // Metodo GET por ID | GET /api/negocio/id
        [HttpGet("{id}")]
        public async Task<IActionResult> GetNegocioPorId(int id)
        {
            var negocio = await _context.Negocios
            .Where(n => n.Id == id)
            .Select(n => new NegocioRespuestaDTO    // No utilizamos FindAsync() para tener control de lo que se muestra
            {
                Id = n.Id,
                Nombre = n.Nombre,
                Telefono = n.Telefono,
                Email = n.Email,
                Direccion = n.Direccion,
                UsaProfesionales = n.UsaProfesionales,
                UsaCategorias = n.UsaCategorias,
                UsaRecordatorios = n.UsaRecordatorios,
                UsaSenia = n.UsaSenia
            }).FirstOrDefaultAsync();

            if(negocio == null)
            {
                return NotFound(new
                {
                    mensaje = $"No existe un negocio con ID {id}."
                });
            }

            return Ok(negocio);
        }

        // Motodo POST
        [HttpPost]
        public async Task<IActionResult> CrearNegocio(NegocioDTO negocioDTO)
        {
            var negocio = new Negocio   // Crea una nueva entidad
            {
                Nombre = negocioDTO.Nombre,
                Telefono = negocioDTO.Telefono,
                Email = negocioDTO.Email,
                Direccion = negocioDTO.Direccion,

                UsaProfesionales = negocioDTO.UsaProfesionales,
                UsaCategorias = negocioDTO.UsaCategorias,
                UsaRecordatorios = negocioDTO.UsaRecordatorios,
                UsaSenia = negocioDTO.UsaSenia
            };

            _context.Negocios.Add(negocio);

            await _context.SaveChangesAsync();

            return Ok(new
            {
                mensaje = "Negocio creado correctamente",
                id = negocio.Id
            });
        }

        // Metodo PUT
        [HttpPut("{id}")]
        public async Task<IActionResult> ActualizarNegocio(int id, NegocioDTO negocioDTO)
        {
            // Busca el negocio, si no lo encuentra devuelve null
            var negocio = await _context.Negocios.FindAsync(id);

            if (negocio == null)
            {
                return NotFound(new
                {
                    mensaje = $"No existe un negocio con ID {id}."
                });
            }

            // Actualiza cada propiedad con las modificaciones realizadas
            negocio.Nombre = negocioDTO.Nombre;
            negocio.Telefono = negocioDTO.Telefono;
            negocio.Email = negocioDTO.Email;
            negocio.Direccion = negocioDTO.Direccion;
            negocio.UsaProfesionales = negocioDTO.UsaProfesionales;
            negocio.UsaCategorias = negocioDTO.UsaCategorias;
            negocio.UsaRecordatorios = negocioDTO.UsaRecordatorios;
            negocio.UsaSenia = negocioDTO.UsaSenia;

            // EF detecta las propiedades que cambiaron para generar la consulta sql internamente
            await _context.SaveChangesAsync();

            return Ok(new
            {
                mensaje = "Negocio actualizado correctamente."
            });
        }

        // Metodo DELETE
        [HttpDelete("{id}")]
        public async Task<IActionResult> EliminarNegocio(int id)
        {
            // FindAsync() devuelve el objeto
            var negocio = await _context.Negocios.FindAsync(id);

            if (negocio == null)
            {
                return NotFound(new
                {
                    mensaje = $"No existe un negocio con ID {id}."
                });
            }

            // Si el negocio existe, EF genera la consulta internamente
            _context.Negocios.Remove(negocio);

            // SaveChangesAsync() ejecuta el SQL
            await _context.SaveChangesAsync();

            return Ok(new
            {
                mensaje = "Negocio eliminado correctamente."
            });
        }
    }
}