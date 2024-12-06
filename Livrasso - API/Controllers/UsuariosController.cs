using Livrasso___API.Models;
using Livrasso___API.Persistence;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace Livrasso___API.Controllers
{
    [ApiController]
    [Route("api/usuarios")]
    public class UsuariosController : Controller
    {
        private readonly LivrassoDbContext _context;

        public UsuariosController(LivrassoDbContext context)
        {
            _context = context;
        }

        // GET: api/usuarios
        [HttpGet]
        public ActionResult<IEnumerable<UsuarioResponseDTO>> GetUsuarios()
        {
            var usuarios = _context.Usuarios
                .Select(u => new UsuarioResponseDTO
                {
                    Id = u.Id,
                    Nome = u.Nome,
                    Email = u.Email,
                })
                .ToList();

            if (!usuarios.Any())
            {
                return NotFound("Nenhum usuário encontrado.");
            }

            return Ok(usuarios);
        }

        // GET: api/usuarios/5
        [HttpGet("{id}")]
        public ActionResult<UsuarioResponseDTO> GetUsuario(int id)
        {
            var usuario = _context.Usuarios
                .Where(u => u.Id == id)
                .Select(u => new UsuarioResponseDTO
                {
                    Id = u.Id,
                    Nome = u.Nome,
                    Email = u.Email,
                })
                .FirstOrDefault();

            if (usuario == null)
            {
                return NotFound("Usuário não encontrado.");
            }

            return Ok(usuario);
        }

        // POST: api/usuarios
        [HttpPost]
        public ActionResult<UsuarioResponseDTO> PostUsuario([FromBody] UsuarioRequestDTO usuarioDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var usuario = new Usuario
            {
                Nome = usuarioDTO.Nome,
                Email = usuarioDTO.Email,
            };

            usuario.Id = _context.Usuarios.Any() ? _context.Usuarios.Max(l => l.Id) + 1 : 1;

            _context.Usuarios.Add(usuario);

            var usuarioResponse = new UsuarioResponseDTO
            {
                Id = usuario.Id,
                Nome = usuario.Nome,
                Email = usuario.Email,
            };

            return CreatedAtAction(nameof(GetUsuario), new { id = usuario.Id }, usuarioResponse);
        }

        // PUT: api/usuarios/5
        [HttpPut("{id}")]
        public ActionResult<UsuarioResponseDTO> PutUsuario(int id, [FromBody] UsuarioRequestDTO usuarioDTO)
        {
            var usuario = _context.Usuarios.FirstOrDefault(u => u.Id == id);

            if (usuario == null)
            {
                return NotFound("Usuário não encontrado.");
            }

            usuario.Nome = usuarioDTO.Nome;
            usuario.Email = usuarioDTO.Email;

            var usuarioResponse = new UsuarioResponseDTO
            {
                Id = usuario.Id,
                Nome = usuario.Nome,
                Email = usuario.Email,
            };

            return Ok(usuarioResponse);
        }

        // DELETE: api/usuarios/5
        [HttpDelete("{id}")]
        public ActionResult DeleteUsuario(int id)
        {
            var usuario = _context.Usuarios.FirstOrDefault(u => u.Id == id);

            if (usuario == null)
            {
                return NotFound("Usuário não encontrado.");
            }

            _context.Usuarios.Remove(usuario);

            return NoContent();
        }

    }
}
