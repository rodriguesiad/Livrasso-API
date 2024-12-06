using System.ComponentModel.DataAnnotations;

namespace Livrasso___API.Models
{
    public class Editora
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public int? AnoFundacao { get; set; }
        public string Pais { get; set; }


        // Relacionamento
        public virtual ICollection<Livro> Livros { get; set; }
    }

    public class EditoraRequestDTO
    {
        [Required(ErrorMessage = "O nome da editora é obrigatório.")]
        [StringLength(100, ErrorMessage = "O nome da editora deve ter no máximo 100 caracteres.")]
        public string Nome { get; set; }

        [Range(1450, 2024, ErrorMessage = "O ano de fundação deve estar entre 1450 e o ano atual.")]
        public int? AnoFundacao { get; set; }

        [StringLength(50, ErrorMessage = "O país deve ter no máximo 50 caracteres.")]
        public string Pais { get; set; }
    }

    public class EditoraResponseDTO
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public int? AnoFundacao { get; set; }
        public string Pais { get; set; }
    }

}
