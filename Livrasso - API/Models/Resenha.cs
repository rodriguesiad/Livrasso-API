using System.ComponentModel.DataAnnotations;

namespace Livrasso___API.Models
{
    public class Resenha
    {
        public int Id { get; set; }
        public string Conteudo { get; set; }
        public float Nota { get; set; }
        public DateTime Data { get; set; }
        public int LivroId { get; set; }
        public int UsuarioId { get; set; }


        // Relacionamentos
        public virtual Livro Livro { get; set; }
        public virtual Usuario Usuario { get; set; }
    }

    public class ResenhaRequestDTO
    {
        [Required(ErrorMessage = "O conteúdo da resenha é obrigatório.")]
        [StringLength(2000, ErrorMessage = "A resenha deve ter no máximo 2000 caracteres.")]
        public string Conteudo { get; set; }

        [Required(ErrorMessage = "O livro da resenha é obrigatório.")]
        public int LivroId { get; set; }

        [Required(ErrorMessage = "O usuário da resenha é obrigatório.")]
        public int UsuarioId { get; set; }
    }

    public class ResenhaResponseDTO
    {
        public int Id { get; set; }
        public string Conteudo { get; set; }
        public float Nota { get; set; }
        public DateTime Data { get; set; }
        public LivroResponseDTO Livro { get; set; }
        public UsuarioResponseDTO Usuario { get; set; }
    }

}
