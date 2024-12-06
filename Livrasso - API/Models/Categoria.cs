using System.ComponentModel.DataAnnotations;

namespace Livrasso___API.Models
{
    public class Categoria
    {
        public int Id { get; set; }
        public string Nome { get; set; }


        // Relacionamento
        public virtual ICollection<Livro> Livros { get; set; }
    }

    public class CategoriaRequestDTO
    {
        [Required(ErrorMessage = "O nome da categoria é obrigatório.")]
        [StringLength(50, ErrorMessage = "O nome da categoria deve ter no máximo 50 caracteres.")]
        public string Nome { get; set; }
    }

    public class CategoriaResponseDTO
    {
        public int Id { get; set; }
        public string Nome { get; set; }
    }

}
