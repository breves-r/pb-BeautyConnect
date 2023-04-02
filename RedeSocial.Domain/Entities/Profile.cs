using System.ComponentModel.DataAnnotations;

namespace RedeSocial.Domain.Entities
{
    public class Profile
    {
        public Profile() 
        {
            this.Posts = new List<Post>();
        }

        public Guid IdProfile { get; set; }

        [Required(ErrorMessage = "Campo 'Nome' Obrigatório")]
        public string Nome { get; set; }

        public string Foto { get; set; }

        public List<Post> Posts { get; set; }
    }
}
