using RedeSocial.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedeSocial.Domain.Interfaces
{
    public interface IFriendshipRepository
    {
        public void Criar(Friendship friendship);
        public ICollection<Friendship> ConsultarTodasDoProfile(Guid IdProfile);
        public bool Exist(Friendship friendship);
        public int DeleteAll(Guid idProfile);

    }
}
