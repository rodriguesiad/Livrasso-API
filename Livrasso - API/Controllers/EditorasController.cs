using Microsoft.AspNetCore.Mvc;
using Livrasso___API.Models;
using Livrasso___API.Persistence;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Livrasso___API.Controllers
{
    [ApiController]
    [Route("api/editoras")]
    public class EditorasController : Controller
    {
        private readonly LivrassoDbContext _context;

        public EditorasController(LivrassoDbContext context)
        {
            _context = context;
        }

        // GET: api/editoras
        [HttpGet]
        public ActionResult<IEnumerable<EditoraResponseDTO>> GetEditoras()
        {
            var editoras = _context.Editoras;

            if (editoras == null || !editoras.Any())
            {
                return NotFound();
            }

            var editorasDto = editoras.Select(e => new EditoraResponseDTO
            {
                Id = e.Id,
                Nome = e.Nome,
                AnoFundacao = e.AnoFundacao,
                Pais = e.Pais
            }).ToList();

            return Ok(editorasDto);
        }

        // GET: api/editoras/5
        [HttpGet("{id}")]
        public ActionResult<EditoraResponseDTO> GetEditora(int id)
        {
            var editora = _context.Editoras.FirstOrDefault(e => e.Id == id);
            if (editora == null)
            {
                return NotFound();
            }

            var editoraDto = new EditoraResponseDTO
            {
                Id = editora.Id,
                Nome = editora.Nome,
                AnoFundacao = editora.AnoFundacao,
                Pais = editora.Pais
            };

            return Ok(editoraDto);
        }

        // POST: api/editoras
        [HttpPost]
        public ActionResult<EditoraResponseDTO> PostEditora([FromBody] EditoraRequestDTO editoraDTO)
        {
            if (editoraDTO == null)
            {
                return BadRequest();
            }

            var editora = new Editora
            {
                Id = _context.Editoras.Any() ? _context.Categorias.Max(c => c.Id) + 1 : 1,
                Nome = editoraDTO.Nome,
                AnoFundacao = editoraDTO.AnoFundacao,
                Pais = editoraDTO.Pais
            };

            _context.Editoras.Add(editora);

            var editoraResponse = new EditoraResponseDTO
            {
                Id = editora.Id,
                Nome = editora.Nome,
                AnoFundacao = editora.AnoFundacao,
                Pais = editora.Pais
            };

            return CreatedAtAction(nameof(GetEditora), new { id = editora.Id }, editoraResponse);
        }

        // PUT: api/editoras/5
        [HttpPut("{id}")]
        public ActionResult<EditoraResponseDTO> PutEditora(int id, [FromBody] EditoraRequestDTO editoraDTO)
        {
            if (editoraDTO == null)
            {
                return BadRequest();
            }

            var editora = _context.Editoras.FirstOrDefault(e => e.Id == id);

            if (editora == null)
            {
                return NotFound();
            }

            editora.Nome = editoraDTO.Nome;
            editora.AnoFundacao = editoraDTO.AnoFundacao;
            editora.Pais = editoraDTO.Pais;

            var editoraResponse = new EditoraResponseDTO
            {
                Id = editora.Id,
                Nome = editora.Nome,
                AnoFundacao = editora.AnoFundacao,
                Pais = editora.Pais
            };

            return NoContent();
        }

        // DELETE: api/editoras/5
        [HttpDelete("{id}")]
        public ActionResult DeleteEditora(int id)
        {
            var editora = _context.Editoras.FirstOrDefault(e => e.Id == id);
            if (editora == null)
            {
                return NotFound();
            }

            _context.Editoras.Remove(editora);
            return NoContent();
        }
    }
}