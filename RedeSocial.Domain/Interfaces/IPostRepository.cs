using RedeSocial.Domain.Entities;

namespace RedeSocial.Domain.Interfaces
{
    public interface IPostRepository
    {
        public bool Vazio();
        public ICollection<Post> ConsultarTodos();
        public Post Consultar(int id);
        public void Criar(Post post);
        public int Alterar(Post post);
        public int Excluir(Post post);
    }
}
