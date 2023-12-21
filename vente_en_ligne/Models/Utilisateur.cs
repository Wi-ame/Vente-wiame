using System.ComponentModel.DataAnnotations;

namespace vente_en_ligne.Models
{
    public class Utilisateur
    {

        [Key]
        public int ID { get; set; }
        public string Nom { get; set; }
        public string Prenom { get; set; }
        public string Email { get; set; }
        public string Tel { get; set; }
    }
}
