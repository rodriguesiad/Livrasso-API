using Livrasso___API.Models;
using Livrasso___API.Persistence;
using Microsoft.AspNetCore.Mvc;

namespace Livrasso___API.Controllers
{

    [ApiController]
    [Route("api/categorias")]
    public class CategoriasController : Controller
    {
        private readonly LivrassoDbContext _context;

        public CategoriasController(LivrassoDbContext context)
        {
            _context = context;
        }

        // GET: api/categorias
        [HttpGet]
        public ActionResult<IEnumerable<CategoriaResponseDTO>> GetCategorias()
        {
            var categorias = _context.Categorias;

            if (categorias == null || !categorias.Any())
            {
                return NotFound();
            }

            var categoriaDtos = categorias.Select(c => new CategoriaResponseDTO
            {
                Id = c.Id,
                Nome = c.Nome
            }).ToList();

            return Ok(categoriaDtos);
        }

        // GET: api/categorias/5
        [HttpGet("{id}")]
        public ActionResult<CategoriaResponseDTO> GetCategoria(int id)
        {
            var categoria = _context.Categorias.FirstOrDefault(c => c.Id == id);
            if (categoria == null)
            {
                return NotFound();
            }

            var categoriaDto = new CategoriaResponseDTO
            {
                Id = categoria.Id,
                Nome = categoria.Nome
            };

            return Ok(categoriaDto);
        }

        // POST: api/categorias
        [HttpPost]
        public ActionResult<CategoriaResponseDTO> PostCategoria([FromBody] CategoriaRequestDTO categoriaDTO)
        {
            if (categoriaDTO == null)
            {
                return BadRequest();
            }

            var novaCategoria = new Categoria
            {
                Id = _context.Categorias.Any() ? _context.Categorias.Max(c => c.Id) + 1 : 1,
                Nome = categoriaDTO.Nome
            };

            _context.Categorias.Add(novaCategoria);

            var categoriaResponse = new CategoriaResponseDTO
            {
                Id = novaCategoria.Id,
                Nome = novaCategoria.Nome
            };

            return CreatedAtAction(nameof(GetCategoria), new { id = novaCategoria.Id }, categoriaResponse);

        }

        // PUT: api/categorias/5
        [HttpPut("{id}")]
        public ActionResult<CategoriaResponseDTO> PutCategoria(int id, [FromBody] CategoriaRequestDTO categoriaDTO)
        {
            if (categoriaDTO == null)
            {
                return BadRequest();
            }

            var categoria = _context.Categorias.FirstOrDefault(c => c.Id == id);
            if (categoria == null)
            {
                return NotFound();
            }

            
            categoria.Nome = categoriaDTO.Nome;
            return NoContent();
        }

        // DELETE: api/categorias/5
        [HttpDelete("{id}")]
        public ActionResult DeleteCategoria(int id)
        {
            var categoria = _context.Categorias.FirstOrDefault(c => c.Id == id);
            if (categoria == null)
            {
                return NotFound();
            }

            _context.Categorias.Remove(categoria);
            return NoContent();
        }

    }
}
