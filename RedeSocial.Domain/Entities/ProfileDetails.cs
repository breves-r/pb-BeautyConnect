using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedeSocial.Domain.Entities
{
    public class ProfileDetails
    {
        [Key]
        public int Id { get; set; }

        public Guid ProfileId { get; set; }
        public Profile Profile { get; set; }

        [Required(ErrorMessage = "Campo 'Aniversário' Obrigatório")]
        public DateTime Aniversario { get; set; }

        [Required(ErrorMessage = "Campo 'TipoPele' Obrigatório")]
        public string TipoPele { get; set; }

        [Required(ErrorMessage = "Campo 'TipoCabelo' Obrigatório")]
        public string TipoCabelo { get; set; }

        [Required(ErrorMessage = "Campo 'CorPele' Obrigatório")]
        public string CorPele { get; set; }

        [Required(ErrorMessage = "Campo 'CorCabelo' Obrigatório")]
        public string CorCabelo { get; set; }
    }
}
