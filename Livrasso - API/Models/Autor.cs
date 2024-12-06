using Livrasso___API.Validations;
using System.ComponentModel.DataAnnotations;

namespace Livrasso___API.Models
{
    public class Autor
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public DateTime? DataDeNascimento { get; set; }
        public string Biografia { get; set; }


        // Relacionamento
        public virtual ICollection<Livro> Livros { get; set; }
    }

    public class AutorRequestDTO
    {
        [Required(ErrorMessage = "O nome é obrigatório.")]
        [StringLength(100, ErrorMessage = "O nome deve ter no máximo 100 caracteres.")]
        public string Nome { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date, ErrorMessage = "Data inválida")]
        [MaxDateAttribute(ErrorMessage = "A data de nascimento não pode ser maior que a data de hoje.")]
        public DateTime? DataDeNascimento { get; set; }

        [StringLength(500, ErrorMessage = "A biografia deve ter no máximo 500 caracteres.")]
        public string Biografia { get; set; }
    }

    public class AutorResponseDTO
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public DateTime? DataDeNascimento { get; set; }
        public string Biografia { get; set; }
    }

}
