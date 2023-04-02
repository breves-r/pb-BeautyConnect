using Microsoft.EntityFrameworkCore;
using RedeSocial.Domain.Entities;
using RedeSocial.Domain.Interfaces;
using RedeSocial.Infra.Context;

namespace RedeSocial.Infra.Repositories
{
    public class ProfileRepository : IProfileRepository
    {
        private readonly RedeSocialDbContext _context;

        public ProfileRepository(RedeSocialDbContext context)
        {
            _context = context;
        }

        public ICollection<Profile> ConsultarTodos()
        {
            return _context.Profiles.Include(x => x.Posts).Include(x => x.FriendshipsA).Include(x => x.FriendshipsB).ToList();
        }

        public bool Vazio()
        {
            if (_context.Profiles == null) 
            {
                return true;
            }

            return false;
        }

        public Profile Consultar(Guid id)
        {
            return _context.Profiles.Include(x => x.Posts).FirstOrDefault(x => x.IdProfile == id);
        }

        public void Criar(Profile profile)
        {
            _context.Profiles.Add(profile);
            _context.SaveChanges();
        }

        public int Alterar(Profile profile)
        {
            _context.Profiles.Update(profile);
            return _context.SaveChanges();
        }

        public int Excluir(Profile profile)
        {
            _context.Profiles.Remove(profile);
            return _context.SaveChanges();
        }
    }
}
