using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AksDemoApp.Models.SubCategory
{
    public class SubCategories
    {
        [ForeignKey("Categories")]
        public int CategoryId { get; set; }
        [Key]

        public int SubCategoryId { get; set; }
        public string? CategoryName { get; set; }
        [Required]
        public string SubCategoryName { get; set; }
        [Required]
        [Display(Name = "Status")]
        public string SubCategoryStatus { get; set; }
    }
}
