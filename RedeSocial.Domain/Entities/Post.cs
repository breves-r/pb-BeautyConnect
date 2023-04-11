using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace RedeSocial.Domain.Entities
{
    public class Post
    {
        public Post() { }

        public Post(int id, Profile profile, DateTime createdDate, string descricao, string imagem, string produto, string categoria, List<Comment> comments)
        {
            Id = id;
            Profile = profile;
            CreatedDate = createdDate;
            Descricao = descricao;
            Imagem = imagem;
            Produto = produto;
            Categoria = categoria;
            Comments = comments;
        }

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
