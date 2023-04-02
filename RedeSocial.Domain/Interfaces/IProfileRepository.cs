using RedeSocial.Domain.Entities;

namespace RedeSocial.Domain.Interfaces
{
    public interface IProfileRepository
    {
        public ICollection<Profile> ConsultarTodos();
        public bool Vazio();
        public Profile Consultar(Guid id);
        public void Criar(Profile profile);
        public int Alterar(Profile profile);
        public int Excluir(Profile profile);
    }
}
