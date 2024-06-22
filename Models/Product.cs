using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ajay.PMS.Models
{
    public class Product : BaseEntity
    {

        [Required]
        [Display(Name = "Product Name")]
        public string ProductName { get; set; }

        [Required]
        [Display(Name = "Manufacturer")]
        public string Manufacturer { get; set; }

        [NotMapped]
        [Display(Name = "Production Date")]
        public DateTime ProductionDate { get; set; }

        [NotMapped]
        [Display(Name = "Expiry Date")]
        public DateTime ExpiryDate { get; set; }

        [Required]
        [Display(Name = "Batch No")]
        public string BatchNo {  get; set; }

        [Required]
        [Display(Name = "Rate")]
        public int Rate {  get; set; }
        public int CategoryId {  get; set; }
        public string ProductUrl { get; set; }
      
        [NotMapped]
        public IFormFile ProductFile { get; set; }

        [Required]
        [Display(Name = "Is Active")]
        public bool IsActive { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public virtual Category Category { get; set; }
    }
}