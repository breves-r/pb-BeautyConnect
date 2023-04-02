using RedeSocial.Domain.Entities;
using RedeSocial.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedeSocial.Domain.Services
{
    public class FriendshipService
    {

        private readonly IFriendshipRepository _friendshipRepository;

        public FriendshipService(IFriendshipRepository friendshipRepository)
        {
            _friendshipRepository = friendshipRepository;
        }

        public bool CriarAmizade(Guid IdProfileA, Guid IdProfileB)
        {
            Friendship friendship = new Friendship();
            friendship.IdProfileA = IdProfileA;
            friendship.IdProfileB = IdProfileB;

            if(_friendshipRepository.Exist(friendship))
            {
                return false;
            }

            _friendshipRepository.Criar(friendship);

            return true;
        }

        public ICollection<Friendship> GetFriends(Guid IdProfileA) {
            return _friendshipRepository.ConsultarTodasDoProfile(IdProfileA);
        }

        public bool ExcluirTodasAmizades(Guid profileId)
        {
            int cont = _friendshipRepository.DeleteAll(profileId);

            return cont == 0 ? false : true;
        }
    }
}
