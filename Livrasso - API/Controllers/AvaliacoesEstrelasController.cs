using Livrasso___API.Models;
using Livrasso___API.Persistence;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace Livrasso___API.Controllers
{
    [ApiController]
    [Route("api/avaliacoes")]
    public class AvaliacoesEstrelasController : ControllerBase
    {
        private readonly LivrassoDbContext _context;

        public AvaliacoesEstrelasController(LivrassoDbContext context)
        {
            _context = context;
        }

        // GET: api/avaliacoes
        [HttpGet]
        public ActionResult<IEnumerable<AvaliacaoEstrelasResponse>> GetAvaliacoes()
        {
            var avaliacoes = _context.AvaliacoesEstrelas
                .Select(a => new AvaliacaoEstrelasResponse
                {
                    Id = a.Id,
                    Estrelas = a.Estrelas,
                    Data = a.Data,
                    Usuario = new UsuarioResponseDTO
                    {
                        Id = a.UsuarioId,
                        Nome = a.Usuario.Nome,
                        Email = a.Usuario.Email
                    },
                    Livro = new LivroResponseDTO
                    {
                        Id = a.LivroId,
                        Titulo = a.Livro.Titulo
                    }
                })
                .ToList();

            if (!avaliacoes.Any())
            {
                return NotFound("Nenhuma avaliação encontrada.");
            }

            return Ok(avaliacoes);
        }

        // GET: api/avaliacoes/5
        [HttpGet("{id}")]
        public ActionResult<AvaliacaoEstrelasResponse> GetAvaliacao(int id)
        {
            var avaliacao = _context.AvaliacoesEstrelas
                .Where(a => a.Id == id)
                .Select(a => new AvaliacaoEstrelasResponse
                {
                    Id = a.Id,
                    Estrelas = a.Estrelas,
                    Data = a.Data,
                    Usuario = new UsuarioResponseDTO
                    {
                        Id = a.UsuarioId,
                        Nome = a.Usuario.Nome,
                        Email = a.Usuario.Email
                    },
                    Livro = new LivroResponseDTO
                    {
                        Id = a.LivroId,
                        Titulo = a.Livro.Titulo
                    }
                })
                .FirstOrDefault();

            if (avaliacao == null)
            {
                return NotFound($"Avaliação de ID {id} não encontrada.");
            }

            return Ok(avaliacao);
        }


        // GET: api/avaliacoes/livro/5
        [HttpGet("livro/{livroId}")]
        public ActionResult<IEnumerable<AvaliacaoEstrelasResponse>> GetAvaliacoesPorLivro(int livroId)
        {
            var avaliacoes = _context.AvaliacoesEstrelas
                .Where(a => a.LivroId == livroId)
                .Select(a => new AvaliacaoEstrelasResponse
                {
                    Id = a.Id,
                    Estrelas = a.Estrelas,
                    Data = a.Data,
                    Usuario = new UsuarioResponseDTO
                    {
                        Id = a.UsuarioId,
                        Nome = a.Usuario.Nome,
                        Email = a.Usuario.Email
                    },
                    Livro = new LivroResponseDTO
                    {
                        Id = a.LivroId,
                        Titulo = a.Livro.Titulo
                    }
                })
                .ToList();

            if (!avaliacoes.Any())
            {
                return NotFound($"Nenhuma avaliação encontrada para o livro com ID {livroId}.");
            }

            return Ok(avaliacoes);
        }

        // POST: api/avaliacoes
        [HttpPost]
        public ActionResult<AvaliacaoEstrelasResponse> PostAvaliacao([FromBody] AvaliacaoEstrelasRequestDTO request)
        {
            if (request == null)
            {
                return BadRequest("Dados de avaliação inválidos.");
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

            var novaAvaliacao = new AvaliacaoEstrelas
            {
                Estrelas = request.Estrelas,
                Data = DateTime.Now,
                UsuarioId = request.UsuarioId,
                Usuario = usuario,
                LivroId = request.LivroId,
                Livro = livro,
            };

            novaAvaliacao.Id = _context.AvaliacoesEstrelas.Any() ? _context.AvaliacoesEstrelas.Max(l => l.Id) + 1 : 1;

            _context.AvaliacoesEstrelas.Add(novaAvaliacao);

            var response = new AvaliacaoEstrelasResponse
            {
                Id = novaAvaliacao.Id,
                Estrelas = novaAvaliacao.Estrelas,
                Data = novaAvaliacao.Data,
                Usuario = new UsuarioResponseDTO
                {
                    Id = novaAvaliacao.UsuarioId,
                    Nome = novaAvaliacao.Usuario.Nome,
                    Email = novaAvaliacao.Usuario.Email
                },
                Livro = new LivroResponseDTO
                {
                    Id = novaAvaliacao.LivroId,
                    Titulo = novaAvaliacao.Livro.Titulo
                }
            };

            return CreatedAtAction(nameof(GetAvaliacao), new { id = novaAvaliacao.Id }, response);
        }

    }
}