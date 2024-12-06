using Microsoft.AspNetCore.Mvc;
using Livrasso___API.Models;
using Livrasso___API.Persistence;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Livrasso___API.Controllers
{
    [ApiController]
    [Route("api/autores")]
    public class AutoresController : Controller
    {
        private readonly LivrassoDbContext _context;

        public AutoresController(LivrassoDbContext context)
        {
            _context = context;
        }

        // GET: api/autores
        [HttpGet]
        public ActionResult<IEnumerable<AutorResponseDTO>> GetAutores()
        {
            var autores = _context.Autores;

            if (autores == null || !autores.Any())
            {
                return NotFound();
            }

            var autoresDto = autores.Select(e => new AutorResponseDTO
            {
                Id = e.Id,
                Nome = e.Nome,
                DataDeNascimento = e.DataDeNascimento,
                Biografia = e.Biografia
            }).ToList();

            return Ok(autoresDto);
        }

        // GET: api/autores/5
        [HttpGet("{id}")]
        public ActionResult<AutorResponseDTO> GetAutor(int id)
        {
            var autor = _context.Autores.FirstOrDefault(e => e.Id == id);
            if (autor == null)
            {
                return NotFound();
            }

            var autorDto = new AutorResponseDTO
            {
                Id = autor.Id,
                Nome = autor.Nome,
                DataDeNascimento = autor.DataDeNascimento,
                Biografia = autor.Biografia
            };

            return Ok(autorDto);
        }

        // POST: api/autores
        [HttpPost]
        public ActionResult<AutorResponseDTO> PostAutor([FromBody] AutorRequestDTO autorDTO)
        {
            if (autorDTO == null)
            {
                return BadRequest();
            }

            var autor = new Autor
            {
                Id = _context.Autores.Any() ? _context.Categorias.Max(c => c.Id) + 1 : 1,
                Nome = autorDTO.Nome,
                DataDeNascimento = autorDTO.DataDeNascimento,
                Biografia = autorDTO.Biografia
            };

            _context.Autores.Add(autor);

            var autorResponse = new AutorResponseDTO
            {
                Id = autor.Id,
                Nome = autor.Nome,
                DataDeNascimento = autor.DataDeNascimento,
                Biografia = autor.Biografia
            };

            return CreatedAtAction(nameof(GetAutor), new { id = autor.Id }, autorResponse);
        }

        // PUT: api/autores/5
        [HttpPut("{id}")]
        public ActionResult<AutorResponseDTO> PutAutor(int id, [FromBody] AutorRequestDTO autorDTO)
        {
            if (autorDTO == null)
            {
                return BadRequest();
            }

            var autor = _context.Autores.FirstOrDefault(e => e.Id == id);

            if (autor == null)
            {
                return NotFound();
            }

            autor.Nome = autorDTO.Nome;
            autor.DataDeNascimento = autorDTO.DataDeNascimento;
            autor.Biografia = autorDTO.Biografia;

            var autorResponse = new AutorResponseDTO
            {
                Id = autor.Id,
                Nome = autor.Nome,
                DataDeNascimento = autor.DataDeNascimento,
                Biografia = autor.Biografia
            };

            return NoContent();
        }

        // DELETE: api/autores/5
        [HttpDelete("{id}")]
        public ActionResult DeleteAutor(int id)
        {
            var autor = _context.Autores.FirstOrDefault(e => e.Id == id);
            if (autor == null)
            {
                return NotFound();
            }

            _context.Autores.Remove(autor);
            return NoContent();
        }
    }
}