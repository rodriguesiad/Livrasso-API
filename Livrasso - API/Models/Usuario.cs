using System.ComponentModel.DataAnnotations;

namespace Livrasso___API.Models
{
    public class Usuario
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }

        // Relacionamento
        public virtual ICollection<AvaliacaoEstrelas> AvaliacoesEstrelas { get; set; }
        public virtual ICollection<Resenha> Resenhas { get; set; }
    }

    public class UsuarioRequestDTO
    {
        [Required(ErrorMessage = "O nome do usuário é obrigatório.")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "O email do usuário é obrigatório.")]
        public string Email { get; set; }

    }

    public class UsuarioResponseDTO
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
    }
}
