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
    public class FriendshipRepository : IFriendshipRepository
    {
        private readonly RedeSocialDbContext _context;

        public FriendshipRepository(RedeSocialDbContext context)
        {
            _context = context;
        }

        public ICollection<Friendship> ConsultarTodasDoProfile(Guid IdProfile)
        {
            return _context.Friendships.Include(x => x.ProfileA).Include(x => x.ProfileB).Where(x => x.IdProfileA == IdProfile || x.IdProfileB == IdProfile).ToList();
        }

        public void Criar(Friendship friendship)
        {
            _context.Friendships.Add(friendship);
            _context.SaveChanges();
        }

        public bool Exist(Friendship friendship)
        {
            Friendship friends = _context.Friendships.Find(friendship.IdProfileA, friendship.IdProfileB);

            if(friends != null)
                return true;

            friends = _context.Friendships.Find(friendship.IdProfileB, friendship.IdProfileA);

            if (friends != null)
                return true;

            return false;
        }

        public int DeleteAll(Guid idProfile)
        {
            ICollection<Friendship> friends = this.ConsultarTodasDoProfile(idProfile);

            foreach (var friend in friends)
            {
                _context.Friendships.Remove(friend);
            }

            return _context.SaveChanges();
        }
    }
}
