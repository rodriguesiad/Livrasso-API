using System.ComponentModel.DataAnnotations;

namespace Livrasso___API.Models
{
    public class AvaliacaoEstrelas
    {
        public int Id { get; set; }
        public int Estrelas { get; set; }
        public DateTime Data { get; set; }
        public int UsuarioId { get; set; }
        public int LivroId { get; set; }


        // Relacionamentos
        public virtual Usuario Usuario { get; set; }
        public virtual Livro Livro { get; set; }
    }

    public class AvaliacaoEstrelasRequestDTO
    {
        [Range(1, 5, ErrorMessage = "A avaliação deve ser entre 1 e 5 estrelas.")]
        public int Estrelas { get; set; }

        [Required(ErrorMessage = "O livro da avaliação é obrigatório.")]
        public int LivroId { get; set; }

        [Required(ErrorMessage = "O usuário da avaliação é obrigatório.")]
        public int UsuarioId { get; set; }
    }

    public class AvaliacaoEstrelasResponse
    {
        public int Id { get; set; }
        public int Estrelas { get; set; }
        public DateTime Data { get; set; }
        public UsuarioResponseDTO Usuario { get; set; }
        public LivroResponseDTO Livro { get; set; }
    }

}
