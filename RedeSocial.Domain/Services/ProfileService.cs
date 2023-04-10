using RedeSocial.Domain.Entities;
using RedeSocial.Domain.Interfaces;

namespace RedeSocial.Domain.Services
{
    public class ProfileService
    {
        private readonly IProfileRepository _profileRepository;
        private readonly FriendshipService _friendshipService;
        private readonly CommentService _commentService;

        public ProfileService(IProfileRepository profileRepository, FriendshipService friendshipService, CommentService commentService)
        {
            _profileRepository = profileRepository;
            _friendshipService = friendshipService;
            _commentService = commentService;
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
            _friendshipService.ExcluirTodasAmizades(profile.IdProfile);
            _commentService.DeletarCommentsPorProfile(profile.IdProfile);

            int cont = _profileRepository.Excluir(profile);
            if (cont == 0)
            {
                return false;
            }

            return true;
        }


    }
}
