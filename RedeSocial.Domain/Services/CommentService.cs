using RedeSocial.Domain.Entities;
using RedeSocial.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedeSocial.Domain.Services
{
    public class CommentService
    {
        private readonly ICommentRepository _repository;

        public CommentService(ICommentRepository repository)
        {
            _repository = repository;
        }

        public ICollection<Comment> ConsultarComments(int postId)
        {
            return _repository.ConsultarTodosDoPost(postId);
        }

        public void CriarComment(Comment comment)
        {
            _repository.Criar(comment);
        }

        public bool AlterarComment(Comment comment)
        {
            int cont = _repository.Alterar(comment);

            return cont == 0 ? false : true;
        }

        public bool DeletarComment(Comment comment)
        {
            int cont = _repository.Excluir(comment);
            
            return cont == 0 ? false : true;
        }
    }
}
