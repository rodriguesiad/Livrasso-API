using Livrasso___API.Models;
using Livrasso___API.Persistence;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace Livrasso___API.Controllers
{
    [ApiController]
    [Route("api/resenhas")]
    public class ResenhasController : Controller
    {
        private readonly LivrassoDbContext _context;

        public ResenhasController(LivrassoDbContext context)
        {
            _context = context;
        }

        // GET: api/resenhas
        [HttpGet]
        public ActionResult<IEnumerable<ResenhaResponseDTO>> GetResenhas()
        {
            var resenhas = _context.Resenhas
                .Select(r => new ResenhaResponseDTO
                {
                    Id = r.Id,
                    Conteudo = r.Conteudo,
                    Nota = r.Nota,
                    Data = r.Data,
                    Livro = new LivroResponseDTO
                    {
                        Id = r.LivroId,
                        Titulo = r.Livro.Titulo
                    },
                    Usuario = new UsuarioResponseDTO
                    {
                        Id = r.UsuarioId,
                        Nome = r.Usuario.Nome,
                        Email = r.Usuario.Email
                    }
                })
                .ToList();

            if (!resenhas.Any())
            {
                return NotFound("Nenhuma resenha encontrada.");
            }

            return Ok(resenhas);
        }

        // GET: api/resenhas/5
        [HttpGet("{id}")]
        public ActionResult<ResenhaResponseDTO> GetResenha(int id)
        {
            var resenha = _context.Resenhas
                .Where(r => r.Id == id)
                .Select(r => new ResenhaResponseDTO
                {
                    Id = r.Id,
                    Conteudo = r.Conteudo,
                    Nota = r.Nota,
                    Data = r.Data,
                    Livro = new LivroResponseDTO
                    {
                        Id = r.LivroId,
                        Titulo = r.Livro.Titulo
                    },
                    Usuario = new UsuarioResponseDTO
                    {
                        Id = r.UsuarioId,
                        Nome = r.Usuario.Nome,
                        Email = r.Usuario.Email
                    }
                })
                .FirstOrDefault();

            if (resenha == null)
            {
                return NotFound("Resenha não encontrada.");
            }

            return Ok(resenha);
        }

        // POST: api/resenhas
        [HttpPost]
        public ActionResult<ResenhaResponseDTO> PostResenha([FromBody] ResenhaRequestDTO request)
        {
            if (request == null)
            {
                return BadRequest();
            }

            var usuario = _context.Usuarios.Where(u => u.Id == request.UsuarioId).Select(u => u).FirstOrDefault();
            if (usuario == null)
            {
                return BadRequest("Usuário não encontrado.");
            }

            var livro = _context.Livros.Where(l => l.Id == request.LivroId).Select(l => l).FirstOrDefault();
            if (livro == null)
            {
                return BadRequest("Livro não encontrado.");
            }

            var resenha = new Resenha
            {
                Conteudo = request.Conteudo,
                Nota = 5,
                Data = DateTime.Now,
                LivroId = request.LivroId,
                Livro = livro,
                UsuarioId = request.UsuarioId,
                Usuario = usuario,

            };

            resenha.Id = _context.Resenhas.Any() ? _context.Resenhas.Max(l => l.Id) + 1 : 1;

            _context.Resenhas.Add(resenha);

            var response = new ResenhaResponseDTO
            {
                Id = resenha.Id,
                Conteudo = resenha.Conteudo,
                Nota = resenha.Nota,
                Data = resenha.Data,
                Livro = new LivroResponseDTO
                {
                    Id = resenha.LivroId,
                    Titulo = resenha.Livro.Titulo
                },
                Usuario = new UsuarioResponseDTO
                {
                    Id = resenha.UsuarioId,
                    Nome = resenha.Usuario.Nome,
                    Email = resenha.Usuario.Email
                }
            };

            return CreatedAtAction(nameof(GetResenha), new { id = response.Id }, response);
        }

        // PUT: api/resenhas/5
        [HttpPut("{id}")]
        public ActionResult<ResenhaResponseDTO> PutResenha(int id, [FromBody] ResenhaRequestDTO request)
        {
            if (request == null)
            {
                return BadRequest();
            }

            var resenha = _context.Resenhas.FirstOrDefault(r => r.Id == id);
            if (resenha == null)
            {
                return NotFound("Resenha não encontrada.");
            }

            resenha.Conteudo = request.Conteudo;

            var usuario = _context.Usuarios.Where(u => u.Id == request.UsuarioId).Select(u => u).FirstOrDefault();
            if (usuario == null)
            {
                return BadRequest("Usuário não encontrado.");
            }

            resenha.UsuarioId = request.UsuarioId;
            resenha.Usuario = usuario;


            var livro = _context.Livros.Where(l => l.Id == request.LivroId).Select(l => l).FirstOrDefault();
            if (livro == null)
            {
                return BadRequest("Livro não encontrado.");
            }

            resenha.LivroId = request.LivroId;
            resenha.Livro = livro;

            var response = new ResenhaResponseDTO
            {
                Id = resenha.Id,
                Conteudo = resenha.Conteudo,
                Nota = resenha.Nota,
                Data = resenha.Data,
                Livro = new LivroResponseDTO
                {
                    Id = resenha.LivroId,
                    Titulo = resenha.Livro.Titulo
                },
                Usuario = new UsuarioResponseDTO
                {
                    Id = resenha.UsuarioId,
                    Nome = resenha.Usuario.Nome,
                    Email = resenha.Usuario.Email
                }
            };

            return Ok(response);
        }

        // DELETE: api/resenhas/5
        [HttpDelete("{id}")]
        public ActionResult DeleteResenha(int id)
        {
            var resenha = _context.Resenhas.FirstOrDefault(r => r.Id == id);
            if (resenha == null)
            {
                return NotFound("Resenha não encontrada.");
            }

            _context.Resenhas.Remove(resenha);

            return NoContent();
        }
    }
}
