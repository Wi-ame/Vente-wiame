using System.ComponentModel.DataAnnotations;

namespace vente_en_ligne.Models
{
    public class Admin
    {
        [Key]
        public string CIN { get; set; }
        [Required]
        public string password { get; set; }
    }
}
