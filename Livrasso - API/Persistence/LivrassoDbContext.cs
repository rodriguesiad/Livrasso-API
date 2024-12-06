namespace Livrasso___API.Persistence
{
    using Livrasso___API.Models;
    public class LivrassoDbContext
    {
        public List<Livro> Livros { get; set; }
        public List<Resenha> Resenhas { get; set; }
        public List<Categoria> Categorias { get; set; }
        public List<Autor> Autores { get; set; }
        public List<Editora> Editoras { get; set; }
        public List<AvaliacaoEstrelas> AvaliacoesEstrelas { get; set; }
        public List<Usuario> Usuarios { get; set; }

        public LivrassoDbContext()
        {
            Livros = new List<Livro>();
            Resenhas = new List<Resenha>();
            Categorias = new List<Categoria>();
            Autores = new List<Autor>();
            Editoras = new List<Editora>();
            AvaliacoesEstrelas = new List<AvaliacaoEstrelas>();
            Usuarios = new List<Usuario>();
        }
    }
}
