using System.ComponentModel.DataAnnotations;
using static NBD6.Models.Bid;

namespace NBD6.Models
{
    public class Bid : IValidatableObject
    {
        [Key]
        public int BidID { get; set; }

        [Required]
        [Display(Name = "Bid Name")]
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

        //Scrapped together lmao
        public string Notes {  get; set; }
        public bool ClientApproved {  get; set; }
        public bool NBDApproved { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            // Check if the end date is before the start date
            if (BidEnd < BidStart)
            {
                yield return new ValidationResult("Bid End date cannot end before it starts.", new[] { "BidEnd" });
            }
        }

        //Material fields
        [Display(Name = "Type")]
        [Required(ErrorMessage = "Material type is required.")]
        public string MaterialType { get; set; }

        [Display(Name = "Quantity")]
        [RegularExpression(@"^(?!-)\d+(\.\d{1,2})?$", ErrorMessage = "Quantity must be a valid number.")]
        public int MaterialQuantity { get; set; }

        [Display(Name = "Description")]
        [RegularExpression(@"^[a-zA-Z0-9\s]+$")]
        public string MaterialDescription { get; set; }

        [Display(Name = "Size")]
        [Required(ErrorMessage = "Please provide a unit of measurement after the digit (cm, m, l, g, kg)")]
        [RegularExpression(@"^(?:\d*\.?\d+)\s*(cm|m|l|g|kg|cubic\s*cm|cubic\s*m)$")]
        public string MaterialSize { get; set; }

        [Display(Name = "Price")]
        [RegularExpression(@"^(?!-)\d+(\.\d{1,2})?$", ErrorMessage = "Unit Price must be a valid number.")]
        public decimal MaterialPrice { get; set; }

        public decimal ExtendedMaterialPrice => MaterialQuantity * MaterialPrice;

        //Labour Fields
        [Required]
        [Display(Name = "Hours")]
        [RegularExpression(@"^(?!-)\d+(\.\d{1,2})?$", ErrorMessage = "Hours must be a valid number.")]
        public double LabourHours { get; set; }

        [Required]
        [Display(Name = "Description")]
        [RegularExpression(@"^[a-zA-Z0-9\s]+$")]
        public string LabourDescription { get; set; }

        [Required]
        [Display(Name = "Price")]
        [RegularExpression(@"^(?!-)\d+(\.\d{1,2})?$", ErrorMessage = "Unit Price must be a valid number.")]
        public decimal LabourPrice { get; set; }

        public decimal ExtendedLabourPrice => (decimal)LabourHours * LabourPrice;

        //Collections
        public virtual ICollection<Staff> Staffs { get; set; }

        //FK
        public int ProjectID { get; set; }
        [Display(Name = "Project")]
        public Project project { get; set; }
    }    
}
