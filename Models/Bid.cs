using System.ComponentModel.DataAnnotations;
using static NBD6.Models.Bid;

namespace NBD6.Models
{
    public class Bid : IValidatableObject
    {
        [Key]
        public int BidID { get; set; }

        [Required]
        [RegularExpression(@"^[a-zA-Z0-9\s-]+$")]
        public string BidName { get; set; }

        [Display(Name = "Start Date")]
        [DataType(DataType.Date)]
        [Required(ErrorMessage = "You must enter an Start date.")]
        public DateTime BidStart { get; set; }

        [Display(Name = "End Date")]
        [DataType(DataType.Date)]
        [Required(ErrorMessage = "You must enter an end date.")]
        public DateTime BidEnd { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            // Check if the end date is before the start date
            if (BidEnd < BidStart)
            {
                yield return new ValidationResult("Bid End date cannot end before it starts.", new[] { "BidEnd" });
            }
        }

        //Material fields
        [Required(ErrorMessage = "Material type is required.")]
        public string MaterialType { get; set; }

        [RegularExpression(@"^(?!-)\d+(\.\d{1,2})?$", ErrorMessage = "Quantity must be a valid number.")]
        public int MaterialQuantity { get; set; }

        [RegularExpression(@"^[a-zA-Z0-9\s]+$")]
        public string MaterialDescription { get; set; }

        [RegularExpression(@"^(?:\d*\.?\d+)\s*(cm|m|l|g|kg|cubic\s*cm|cubic\s*m)$")]
        public string MaterialSize { get; set; }

        [RegularExpression(@"^(?!-)\d+(\.\d{1,2})?$", ErrorMessage = "Unit Price must be a valid number.")]
        public decimal MaterialPrice { get; set; }

        public decimal ExtendedMaterialPrice => MaterialQuantity * MaterialPrice;

        //Labour Fields
        [Required]
        [RegularExpression(@"^(?!-)\d+(\.\d{1,2})?$", ErrorMessage = "Hours must be a valid number.")]
        public double LabourHours { get; set; }

        [Required]
        [RegularExpression(@"^[a-zA-Z0-9\s]+$")]
        public string LabourDescription { get; set; }

        [Required]
        [RegularExpression(@"^(?!-)\d+(\.\d{1,2})?$", ErrorMessage = "Unit Price must be a valid number.")]
        public decimal LabourPrice { get; set; }

        public decimal ExtendedLabourPrice => (decimal)LabourHours * LabourPrice;

        //Collections
        public virtual ICollection<Staff> Staffs { get; set; }

        //FK
        public int ProjectID { get; set; }
        public Project project { get; set; }
    }    
}
