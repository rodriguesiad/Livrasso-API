using Livrasso___API.Models;
using Livrasso___API.Persistence;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Collections.Generic;

namespace Livrasso___API.Controllers
{
    [ApiController]
    [Route("api/livros")]
    public class LivrosController : Controller
    {
        private readonly LivrassoDbContext _context;

        public LivrosController(LivrassoDbContext context)
        {
            _context = context;
        }

        // GET: api/livros
        [HttpGet]
        public ActionResult<IEnumerable<LivroResponseDTO>> GetLivros()
        {
            var livros = _context.Livros
                .Select(l => new LivroResponseDTO
                {
                    Id = l.Id,
                    Titulo = l.Titulo,
                    Autor = new AutorResponseDTO
                    {
                        Id = l.Autor.Id,
                        Nome = l.Autor.Nome,
                        Biografia = l.Autor.Biografia,
                        DataDeNascimento = l.Autor.DataDeNascimento
                    },
                    Editora = new EditoraResponseDTO
                    {
                        Id = l.Editora.Id,
                        Nome = l.Editora.Nome,
                        AnoFundacao = l.Editora.AnoFundacao,
                        Pais = l.Editora.Pais
                    },
                    AnoPublicacao = l.AnoPublicacao,
                    Genero = l.Genero,
                    Resumo = l.Resumo,
                    Categorias = l.Categorias?.Select(c => new CategoriaResponseDTO
                    {
                        Id = c.Id,
                        Nome = c.Nome
                    }).ToList() ?? new List<CategoriaResponseDTO>()
                })
                .ToList();

            if (!livros.Any())
            {
                return NotFound("Nenhum livro encontrado.");
            }

            return Ok(livros);
        }


        // GET: api/livros/5
        [HttpGet("{id}")]
        public ActionResult<LivroResponseDTO> GetLivro(int id)
        {
            var livro = _context.Livros
                .Where(l => l.Id == id)
                .Select(l => new LivroResponseDTO
                {
                    Id = l.Id,
                    Titulo = l.Titulo,
                    Autor = new AutorResponseDTO
                    {
                        Id = l.Autor.Id,
                        Nome = l.Autor.Nome,
                        Biografia = l.Autor.Biografia,
                        DataDeNascimento = l.Autor.DataDeNascimento
                    },
                    Editora = new EditoraResponseDTO
                    {
                        Id = l.Editora.Id,
                        Nome = l.Editora.Nome,
                        AnoFundacao = l.Editora.AnoFundacao,
                        Pais = l.Editora.Pais
                    },
                    AnoPublicacao = l.AnoPublicacao,
                    Genero = l.Genero,
                    Resumo = l.Resumo,
                    Categorias = l.Categorias?.Select(c => new CategoriaResponseDTO
                    {
                        Id = c.Id,
                        Nome = c.Nome
                    }).ToList() ?? new List<CategoriaResponseDTO>()
                })
                .FirstOrDefault();

            if (livro == null)
            {
                return NotFound($"Livro com ID {id} não encontrado.");
            }

            return Ok(livro);
        }

        // POST: api/livros
        [HttpPost]
        public ActionResult<LivroResponseDTO> PostLivro(LivroRequestDTO livroRequestDTO)
        {
            if (livroRequestDTO == null)
            {
                return BadRequest();
            }

            // Verifica se as categorias fornecidas existem
            var categorias = _context.Categorias
                .Where(c => livroRequestDTO.IdsCategorias.Contains(c.Id))
                .ToList();

            if (categorias.Count != livroRequestDTO.IdsCategorias.Count)
            {
                return BadRequest("Uma ou mais categorias não foram encontradas.");
            }

            var autor = _context.Autores.Where(a => a.Id == livroRequestDTO.AutorId).Select(a => a).FirstOrDefault();
            if (autor == null)
            {
                return BadRequest("Autor não encontrado.");
            }

            var editora = _context.Editoras.Where(e => e.Id == livroRequestDTO.EditoraId).Select(e => e).FirstOrDefault();
            if (editora == null)
            {
                return BadRequest("Editora não encontrada.");
            }

            // Cria o novo livro
            var livro = new Livro
            {
                Titulo = livroRequestDTO.Titulo,
                AnoPublicacao = livroRequestDTO.AnoPublicacao,
                Genero = livroRequestDTO.Genero,
                Resumo = livroRequestDTO.Resumo,
                Categorias = categorias,
                AutorId = livroRequestDTO.AutorId,
                Autor = autor,
                EditoraId = livroRequestDTO.EditoraId,
                Editora = editora
            };

            // Atribui um novo ID ao livro (simulando o comportamento de banco)
            livro.Id = _context.Livros.Any() ? _context.Livros.Max(l => l.Id) + 1 : 1;

            // Adiciona o livro à lista
            _context.Livros.Add(livro);

            return CreatedAtAction(nameof(GetLivro), new { id = livro.Id }, new LivroResponseDTO
            {
                Id = livro.Id,
                Titulo = livro.Titulo,
                Autor = new AutorResponseDTO
                {
                    Id = livro.Autor.Id,
                    Nome = livro.Autor.Nome,
                    Biografia = livro.Autor.Biografia,
                    DataDeNascimento = livro.Autor.DataDeNascimento
                },
                Editora = new EditoraResponseDTO
                {
                    Id = livro.Editora.Id,
                    Nome = livro.Editora.Nome,
                    AnoFundacao = livro.Editora.AnoFundacao,
                    Pais = livro.Editora.Pais
                },
                AnoPublicacao = livro.AnoPublicacao,
                Genero = livro.Genero,
                Resumo = livro.Resumo,
                Categorias = livro.Categorias?.Select(c => new CategoriaResponseDTO
                {
                    Id = c.Id,
                    Nome = c.Nome
                }).ToList() ?? new List<CategoriaResponseDTO>()
            });
        }

        // PUT: api/livros/5
        [HttpPut("{id}")]
        public ActionResult<LivroResponseDTO> PutLivro(int id, LivroRequestDTO livroRequestDTO)
        {
            if(livroRequestDTO == null)
            {
                return BadRequest();
            }

            var livro = _context.Livros.FirstOrDefault(l => l.Id == id);

            if (livro == null)
            {
                return NotFound($"Livro com ID {id} não encontrado.");
            }

            livro.Titulo = livroRequestDTO.Titulo;
            livro.AnoPublicacao = livroRequestDTO.AnoPublicacao;
            livro.Genero = livroRequestDTO.Genero;
            livro.Resumo = livroRequestDTO.Resumo;

            var categorias = _context.Categorias
                .Where(c => livroRequestDTO.IdsCategorias.Contains(c.Id))
                .ToList();

            if (categorias.Count != livroRequestDTO.IdsCategorias.Count)
            {
                return BadRequest("Uma ou mais categorias não foram encontradas.");
            }

            livro.Categorias = categorias;

            var autor = _context.Autores.Where(a => a.Id == livroRequestDTO.AutorId).Select(a => a).FirstOrDefault();
            if (autor == null)
            {
                return BadRequest("Autor não encontrado.");
            }

            livro.AutorId = livroRequestDTO.AutorId;
            livro.Autor = autor;

            var editora = _context.Editoras.Where(e => e.Id == livroRequestDTO.EditoraId).Select(e => e).FirstOrDefault();
            if (editora == null)
            {
                return BadRequest("Editora não encontrada.");
            }

            livro.EditoraId = livroRequestDTO.EditoraId;
            livro.Editora = editora;

            return Ok(new LivroResponseDTO
            {
                Id = livro.Id,
                Titulo = livro.Titulo,
                Autor = new AutorResponseDTO
                {
                    Id = livro.Autor.Id,
                    Nome = livro.Autor.Nome,
                    Biografia = livro.Autor.Biografia,
                    DataDeNascimento = livro.Autor.DataDeNascimento
                },
                Editora = new EditoraResponseDTO
                {
                    Id = livro.Editora.Id,
                    Nome = livro.Editora.Nome,
                    AnoFundacao = livro.Editora.AnoFundacao,
                    Pais = livro.Editora.Pais
                },
                AnoPublicacao = livro.AnoPublicacao,
                Genero = livro.Genero,
                Resumo = livro.Resumo,
                Categorias = livro.Categorias?.Select(c => new CategoriaResponseDTO
                {
                    Id = c.Id,
                    Nome = c.Nome
                }).ToList() ?? new List<CategoriaResponseDTO>()
            });
        }

        // DELETE: api/livros/5
        [HttpDelete("{id}")]
        public ActionResult DeleteLivro(int id)
        {
            var livro = _context.Livros.FirstOrDefault(l => l.Id == id);

            if (livro == null)
            {
                return NotFound($"Livro com ID {id} não encontrado.");
            }

            _context.Livros.Remove(livro);
            return NoContent();
        }
    }
}