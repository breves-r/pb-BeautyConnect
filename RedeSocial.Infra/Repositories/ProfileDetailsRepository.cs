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
    public class ProfileDetailsRepository : IProfileDetailsRepository
    {
        private readonly RedeSocialDbContext _context;

        public ProfileDetailsRepository(RedeSocialDbContext context)
        {
            _context = context;
        }

        public void Criar(ProfileDetails details)
        {
            _context.ProfileDetails.Add(details);
            _context.SaveChanges();
        }

        public int Alterar(ProfileDetails details)
        {
            _context.ProfileDetails.Update(details);
            return _context.SaveChanges();
        }

        public ProfileDetails Consultar(Guid profileId)
        {
            return _context.ProfileDetails.Include(x => x.Profile).FirstOrDefault(p => p.ProfileId == profileId);
        }

        public bool Vazio(Guid profileId)
        {
            if (_context.ProfileDetails.FirstOrDefault(x => x.ProfileId == profileId) == null)
            {
                return true;
            }

            return false;
        }
    }
}
