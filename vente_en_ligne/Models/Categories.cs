using System.ComponentModel.DataAnnotations;

namespace vente_en_ligne.Models
{
    public class Categories
    {
        [Key]
        public int CategorieID { get; set; }
        public string CategorieName { get; set; }
    }
}
