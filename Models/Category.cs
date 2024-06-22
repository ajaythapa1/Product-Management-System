using System.ComponentModel.DataAnnotations;

namespace Ajay.PMS.Models
{
    public class Category : BaseEntity
    {
        [Required]
        [Display(Name ="Category Name")]
        public string CategoryName { get; set; }
      
        [Required]
        [Display(Name = "Description")]
        public string Description { get; set; }
       
        [Required]
        [Display(Name = "Is Active")]
        public bool IsActive { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }

        public virtual ICollection<Product> Products { get; set;}
    }
}
