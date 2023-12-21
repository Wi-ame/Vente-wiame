using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace vente_en_ligne.Models
{
    public class Produit
    {
        [Key]
        public int IdPr { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public double prix { get; set; }

        [ForeignKey("Categorie")]
        [Required]
        public int IDC { get; set; }

        public Categories Categories { get; set; }

        [ForeignKey("Proprietaire")]
        [Required]
        public string IDP { get; set; }

        public Proprietaire proprietaires { get; set; }

        [Required]
        public DateTime DateDepot { get; set; }

        [Required]
        public byte[] ImageData { get; set; }

        [Required]
        public int stock { get; set; }

        [Required(ErrorMessage = "Please choose an image.")]
        [NotMapped]
        public IFormFile ImageFile { get; set; }
        [NotMapped]
        public List<SelectListItem> CategoriesList { get; set; }
        public Produit()
        {
            CategoriesList = new List<SelectListItem>();
        }

    }

}