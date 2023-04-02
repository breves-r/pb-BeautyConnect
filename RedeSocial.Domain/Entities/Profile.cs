using System.ComponentModel.DataAnnotations;

namespace RedeSocial.Domain.Entities
{
    public class Profile
    {
        public Profile() 
        {
            this.Posts = new List<Post>();
        }

        [Key]
        public Guid IdProfile { get; set; }

        [Required(ErrorMessage = "Campo 'Nome' Obrigatório")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Campo 'Sobrenome' Obrigatório")]
        public string Sobrenome { get; set; }

        [Required(ErrorMessage = "Campo 'Telefone' Obrigatório")]
        public string Telefone { get; set; }

        public string Foto { get; set; }

        public ProfileDetails Details { get; set; }

        public List<Post> Posts { get; set; }

        public ICollection<Friendship> FriendshipsA { get; set; }

        public ICollection<Friendship> FriendshipsB { get; set; }

    }
}
