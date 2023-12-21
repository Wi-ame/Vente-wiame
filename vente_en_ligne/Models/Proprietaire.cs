using System.ComponentModel.DataAnnotations;

namespace vente_en_ligne.Models
{
    public class Proprietaire
    {
        [Key]
        public string INterID { get; set; }

        public string NomEntreprise { get; set; }
        public string AdresseEntreprise { get; set; }

        [Required]
        public string Nom { get; set; }
        [Required]
        [StringLength(10, ErrorMessage = "Le prénom ne peut pas dépasser 10 caractères.")]
        public string Prenom { get; set; }
        [Required]
        [RegularExpression(@"^(\+[0-9]{1,3})?[0-9]{10,15}$", ErrorMessage = "Veuillez saisir un numéro de téléphone valide.")]
        public string Tele { get; set; }
        [Required]
        [EmailAddress(ErrorMessage = "Veuillez saisir une adresse e-mail valide.")]
        public string Email { get; set; }
        [Required]

        public string password { get; set; }


    }
}
