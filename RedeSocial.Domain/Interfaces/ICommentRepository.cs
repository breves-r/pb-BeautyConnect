using RedeSocial.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedeSocial.Domain.Interfaces
{
    public interface ICommentRepository
    {
        public ICollection<Comment> ConsultarTodosDoPost(int postId);
        public Comment Consultar(int id);
        public void Criar(Comment comment);
        public int Alterar(Comment comment);
        public int Excluir(Comment comment);
        public int ExcluirComentariosPorProfile(Guid profileId);
    }
}
