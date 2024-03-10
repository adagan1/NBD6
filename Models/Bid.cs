using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
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

        //FK
        public int ProjectID { get; set; }
        [Display(Name = "Project")]
        public Project Project { get; set; }

        //// Foreign key for Material
        //[ForeignKey("Material")]
        //public int MaterialID { get; set; }
        //[Display(Name = "Material")]
        //public Material Material { get; set; }

        //// Foreign key for Labour
        //[ForeignKey("Labour")]
        //public int LabourID { get; set; }
        //[Display(Name = "Labour")]
        //public Labour Labour { get; set; }

        //Collections
        public virtual ICollection<Staff> Staffs { get; set; }

        // Collection of materials
        public ICollection<Material> Materials { get; set; }

        // Collection of labour
        public ICollection<Labour> Labours { get; set; }
    }    
}
