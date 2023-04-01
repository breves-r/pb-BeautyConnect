using RedeSocial.Domain.Entities;
using RedeSocial.Domain.Interfaces;

namespace RedeSocial.Domain.Services
{
    public class ProfileService
    {
        private readonly IProfileRepository _profileRepository;

        public ProfileService(IProfileRepository profileRepository)
        {
            _profileRepository = profileRepository;
        }

        public ICollection<Profile> ConsultarProfiles()
        {
            return _profileRepository.ConsultarTodos();
        }

        public bool ProfileVazio()
        {
            return _profileRepository.Vazio();
        }

        public Profile ConsultarProfile(Guid id)
        {
            return _profileRepository.Consultar(id);
        }

        public void CriarProfile(Profile profile)
        {
            _profileRepository.Criar(profile);
        }

        public bool AlterarProfile(Profile profile)
        {
            int cont =_profileRepository.Alterar(profile);
            if (cont == 0)
            {
                return false;
            }

            return true;
        }

        public bool ExcluirProfile(Profile profile)
        {
            int cont = _profileRepository.Excluir(profile);
            if (cont == 0)
            {
                return false;
            }

            return true;
        }
    }
}
