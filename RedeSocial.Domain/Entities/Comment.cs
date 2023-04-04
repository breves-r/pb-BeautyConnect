using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedeSocial.Domain.Entities
{
    public class Comment
    {
        public int Id { get; set; }
        public Guid ProfileId { get; set; }
        public Profile Profile { get; set; }
        public int PostId { get; set; }
        public Post Post { get; set; }

        [Required(ErrorMessage = "Campo 'Descricao' Obrigatório")]
        public string Descricao { get; set; }
    }
}
