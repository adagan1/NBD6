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

        // Total bid amount
        public decimal BidAmount
        {
            get
            {
                decimal totalAmount = 0;

                // Calculate total amount from labor
                if (Labours != null)
                {
                    foreach (var labour in Labours)
                    {
                        totalAmount += labour.ExtendedLabourPrice;
                    }
                }

                // Calculate total amount from materials
                if (Materials != null)
                {
                    foreach (var material in Materials)
                    {
                        totalAmount += material.ExtendedMaterialPrice;
                    }
                }

                return totalAmount;
            }
        }

        //Collections
        public virtual ICollection<Labour> Labours { get; set; }
        public virtual ICollection<Material> Materials { get; set; }
        public virtual ICollection<Staff> Staffs { get; set; }

        //FK
        public int ProjectID { get; set; }
        public Project project { get; set; }

        public int LabourID { get; set; }
        public Labour labour { get; set; }

        public int MaterialID { get; set; }
        public Material material { get; set; }
    }

    public class Labour
    {
        public int LabourID { get; set; }

        [Required]
        [RegularExpression(@"^(?!-)\d+(\.\d{1,2})?$", ErrorMessage = "Hours must be a valid number.")]
        public double Hours { get; set; }

        [Required]
        [RegularExpression(@"^[a-zA-Z0-9\s]+$")]
        public string LabourDescription { get; set; }

        [Required]
        [RegularExpression(@"^(?!-)\d+(\.\d{1,2})?$", ErrorMessage = "Unit Price must be a valid number.")]
        public decimal LabourPrice { get; set; }

        public decimal ExtendedLabourPrice => (decimal)Hours * LabourPrice;

        public int BidID { get; set; }
        public Bid bid { get; set; }
    }

    public enum MaterialType
    {
        Plant,
        Pottery,
        General
    }

    public class Material
    {
        public int MaterialID { get; set; }

        [Required(ErrorMessage = "Material type is required.")]
        public MaterialType Type { get; set; }

        [RegularExpression(@"^(?!-)\d+(\.\d{1,2})?$", ErrorMessage = "Quantity must be a valid number.")]
        public int Quantity { get; set; }

        [RegularExpression(@"^[a-zA-Z0-9\s]+$")]
        public string MaterialDescription { get; set; }

        [RegularExpression(@"^(?:\d*\.?\d+)\s*(cm|m|l|g|kg|cubic\s*cm|cubic\s*m)$")]
        public string Size { get; set; }

        [RegularExpression(@"^(?!-)\d+(\.\d{1,2})?$", ErrorMessage = "Unit Price must be a valid number.")]
        public decimal MaterialPrice { get; set; }

        public decimal ExtendedMaterialPrice => Quantity * MaterialPrice;

        public int BidID { get; set; }
        public Bid bid { get; set; }
    }
}
