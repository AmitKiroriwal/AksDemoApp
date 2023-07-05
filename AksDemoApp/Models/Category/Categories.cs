using System.ComponentModel.DataAnnotations;

namespace AksDemoApp.Models.Category
{
    public class Categories
    {
        [Key]
        public int CategoryId { get; set; }
        [Required]
        [Display(Name = "Category Name")]
        public string CategoryName { get; set; }
        [Required]
        [Display(Name = "Status")]
        public string CategoryStatus { get; set; }
    }
}
