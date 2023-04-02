using RedeSocial.Domain.Entities;
using RedeSocial.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedeSocial.Domain.Services
{
    public class ProfileDetailsService
    {
        private readonly IProfileDetailsRepository _repository;

        public ProfileDetailsService(IProfileDetailsRepository repository)
        {
            _repository = repository;
        }

        public ProfileDetails ConsultarProfileDetails(Guid profileId)
        {
            return _repository.Consultar(profileId);
        }

        public void CriarProfileDetails(ProfileDetails profileDetails, Guid profileId)
        {
            profileDetails.ProfileId = profileId;
            _repository.Criar(profileDetails);
        }

        public bool AlterarProfileDetails(ProfileDetails profileDetails)
        {
            int cont = _repository.Alterar(profileDetails);

            return cont == 0 ? false : true;
        }

        public bool ProfileDetailsVazio(Guid profileId)
        {
            return _repository.Vazio(profileId);
        }
    }
}
