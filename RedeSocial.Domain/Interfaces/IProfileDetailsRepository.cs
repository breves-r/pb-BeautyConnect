using RedeSocial.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedeSocial.Domain.Interfaces
{
    public interface IProfileDetailsRepository
    {
        public bool Vazio(Guid profileId);
        public ProfileDetails Consultar(Guid profileId);
        public void Criar(ProfileDetails details);
        public int Alterar(ProfileDetails details);
    }
}
