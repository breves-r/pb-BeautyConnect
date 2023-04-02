using Microsoft.EntityFrameworkCore;
using RedeSocial.Domain.Entities;
using RedeSocial.Domain.Interfaces;
using RedeSocial.Infra.Context;

namespace RedeSocial.Infra.Repositories
{
    public class PostRepository : IPostRepository
    {
        private readonly RedeSocialDbContext _context;

        public PostRepository(RedeSocialDbContext context)
        {
            _context = context;
        }

        public bool Vazio()
        {
            if (_context.Posts == null)
            {
                return true;
            }

            return false;
        }

        public ICollection<Post> ConsultarTodos()
        {
            return _context.Posts.Include(x => x.Profile).ToList();
        }

        public Post Consultar(int id)
        {
            return _context.Posts.Include(x => x.Profile).FirstOrDefault(x => x.Id == id);
        }

        public void Criar(Post post)
        {
            _context.Posts.Add(post);
            _context.SaveChanges();
        }

        public int Alterar(Post post)
        {
            _context.Posts.Update(post);
            return _context.SaveChanges();
        }

        public int Excluir(Post post)
        {
            _context.Posts.Remove(post);
            return _context.SaveChanges();
        }
    }
}
