using Microsoft.EntityFrameworkCore;
using RedeSocial.Domain.Entities;
using RedeSocial.Domain.Interfaces;
using RedeSocial.Infra.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedeSocial.Infra.Repositories
{
    public class CommentRepository : ICommentRepository
    {
        private readonly RedeSocialDbContext _context;

        public CommentRepository(RedeSocialDbContext context)
        {
            _context = context;
        }

        public ICollection<Comment> ConsultarTodosDoPost(int postId)
        {
            return _context.Comments.Include(x => x.Post).Include(x => x.Profile).Where(x => x.PostId == postId).ToList();
        }

        public Comment Consultar(int id)
        {
            return _context.Comments.Include(x => x.Post).Include(x => x.Profile).FirstOrDefault(x => x.Id == id);
        }


        public void Criar(Comment comment)
        {
            _context.Comments.Add(comment);
            _context.SaveChanges();
        }

        public int Alterar(Comment comment)
        {
            _context.Comments.Update(comment);
            return _context.SaveChanges();
        }

        public int Excluir(Comment comment)
        {
            _context.Comments.Remove(comment);
            return _context.SaveChanges();
        }

        public int ExcluirComentariosPorProfile(Guid profileId)
        {
            ICollection<Comment> comments = _context.Comments.Where(x => x.ProfileId == profileId).ToList();

            foreach (var comment in comments)
            {
                _context.Comments.Remove(comment);
            }

            return _context.SaveChanges();
        }
    }
}
