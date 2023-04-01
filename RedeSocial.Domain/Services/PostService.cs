using RedeSocial.Domain.Entities;
using RedeSocial.Domain.Interfaces;

namespace RedeSocial.Domain.Services
{
    public class PostService
    {
        private readonly IPostRepository _postRepository;

        public PostService(IPostRepository postRepository)
        {
            _postRepository = postRepository;
        }

        public bool PostVazio()
        {
            return _postRepository.Vazio();
        }

        public ICollection<Post> ConsultarPosts()
        {
            return _postRepository.ConsultarTodos();
        }

        public Post ConsultarPost(int id) 
        {
            return _postRepository.Consultar(id);
        }

        public void CriarPost(Post post)
        {
            _postRepository.Criar(post);
        }

        public bool AlterarPost(Post post)
        {
            int cont = _postRepository.Alterar(post);
            if (cont == 0)
            {
                return false;
            }

            return true;
        }

        public bool DeletarPost(Post post)
        {
            int cont = _postRepository.Excluir(post);
            if (cont == 0)
            {
                return false;
            }

            return true;
        }
    }
}
