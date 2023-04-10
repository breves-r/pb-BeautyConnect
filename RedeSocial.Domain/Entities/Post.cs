using System.ComponentModel.DataAnnotations;

namespace RedeSocial.Domain.Entities
{
    public class Post
    {
        public int Id { get; set; }

        public Profile Profile { get; set; }

        public DateTime CreatedDate { get; set; } = DateTime.Now;

        [Required(ErrorMessage = "Campo 'Descricao' Obrigatório")]
        public string Descricao { get; set; }

        public string Imagem { get; set; }

        public string Produto { get; set; }

        public string Categoria { get; set; }

        public List<Comment> Comments { get; set; }
    }
}
