using backend.Datos;
using backend.DTOs;
using backend.modelos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoriaController : ControllerBase
    {
        private readonly AppDbContext _context;

        public CategoriaController(AppDbContext context)
        {
            _context = context;
        }

        // GET
        [HttpGet]
        public async Task<IActionResult> GetCategorias()
        {
            var categorias = await _context.Categorias
                .Select(c => new CategoriaRespuestaDTO
                {
                    Id = c.Id,
                    Descripcion = c.Descripcion,
                    Estado = c.Estado
                })
                .ToListAsync();

            return Ok(categorias);
        }

        // GET por ID
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCategoriaPorId(int id)
        {
            var categoria = await _context.Categorias
                .Where(c => c.Id == id)
                .Select(c => new CategoriaRespuestaDTO
                {
                    Id = c.Id,
                    Descripcion = c.Descripcion,
                    Estado = c.Estado
                })
                .FirstOrDefaultAsync();

            if (categoria == null)
            {
                return NotFound(new
                {
                    mensaje = $"No existe una categoría con ID {id}."
                });
            }

            return Ok(categoria);
        }

        // POST
        [HttpPost]
        public async Task<IActionResult> CrearCategoria(CategoriaDTO dto)
        {
            var categoria = new Categoria
            {
                Descripcion = dto.Descripcion,
                Estado = dto.Estado
            };

            _context.Categorias.Add(categoria);

            await _context.SaveChangesAsync();

            return Ok(new
            {
                mensaje = "Categoría creada correctamente.",
                id = categoria.Id
            });
        }

        // PUT
        [HttpPut("{id}")]
        public async Task<IActionResult> ActualizarCategoria(int id, CategoriaDTO dto)
        {
            var categoria = await _context.Categorias.FindAsync(id);

            if (categoria == null)
            {
                return NotFound(new
                {
                    mensaje = $"No existe una categoría con ID {id}."
                });
            }

            categoria.Descripcion = dto.Descripcion;
            categoria.Estado = dto.Estado;

            await _context.SaveChangesAsync();

            return Ok(new
            {
                mensaje = "Categoría actualizada correctamente."
            });
        }

        // DELETE
        [HttpDelete("{id}")]
        public async Task<IActionResult> EliminarCategoria(int id)
        {
            var categoria = await _context.Categorias
                .Include(c => c.Servicios)
                .FirstOrDefaultAsync(c => c.Id == id);

            if (categoria == null)
            {
                return NotFound(new
                {
                    mensaje = $"No existe una categoría con ID {id}."
                });
            }

            if (categoria.Servicios.Any())
            {
                return BadRequest(new
                {
                    mensaje = "No se puede eliminar la categoría porque tiene servicios asociados."
                });
            }

            _context.Categorias.Remove(categoria);

            await _context.SaveChangesAsync();

            return Ok(new
            {
                mensaje = "Categoría eliminada correctamente."
            });
        }
    }
}