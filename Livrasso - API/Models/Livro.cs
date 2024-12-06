using System.ComponentModel.DataAnnotations;

namespace Livrasso___API.Models
{
    public class Livro
    {
        public int Id { get; set; }
        public string Titulo { get; set; }
        public int AutorId { get; set; }
        public int? EditoraId { get; set; }
        public int AnoPublicacao { get; set; }
        public string Genero { get; set; }
        public string Resumo { get; set; }

        // Relacionamentos
        public virtual Autor Autor { get; set; }
        public virtual Editora Editora { get; set; }
        public virtual Resenha Resenha { get; set; }
        public virtual ICollection<Categoria> Categorias { get; set; }
        public virtual ICollection<AvaliacaoEstrelas> AvaliacoesEstrelas { get; set; }
    }

    public class LivroRequestDTO
    {
        [Required(ErrorMessage = "O título do livro é obrigatório.")]
        [StringLength(200, ErrorMessage = "O título do livro deve ter no máximo 200 caracteres.")]
        public string Titulo { get; set; }

        [Required(ErrorMessage = "O autor é obrigatório.")]
        public int AutorId { get; set; }

        [Required(ErrorMessage = "A editora é obrigatória.")]
        public int EditoraId { get; set; }

        [Range(1450, 2024, ErrorMessage = "O ano de publicação deve estar entre 1450 e o ano atual.")]
        public int AnoPublicacao { get; set; }

        [StringLength(50, ErrorMessage = "O gênero deve ter no máximo 50 caracteres.")]
        public string Genero { get; set; }

        [StringLength(1000, ErrorMessage = "O resumo deve ter no máximo 1000 caracteres.")]
        public string Resumo { get; set; }

        public List<int> IdsCategorias { get; set; }
    }

    public class LivroResponseDTO
    {
        public int Id { get; set; }
        public string Titulo { get; set; }
        public AutorResponseDTO Autor { get; set; }
        public EditoraResponseDTO Editora { get; set; }
        public int AnoPublicacao { get; set; }
        public string Genero { get; set; }
        public string Resumo { get; set; }
        public List<CategoriaResponseDTO> Categorias { get; set; }
    }

}
