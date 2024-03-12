using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NBD6.Models
{
    public class Labour
    {
        public int LabourID { get; set; }

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

        //Foreign Key
        public int BidID { get; set; }
        public Bid Bid { get; set; }

    }
}