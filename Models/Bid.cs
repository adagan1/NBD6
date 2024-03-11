using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static NBD6.Models.Bid;

namespace NBD6.Models
{
    public class Bid : IValidatableObject
    {
        [Key]
        [Display(Name = "Bid ID")]
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
        [Display(Name = "Project")]
        public int ProjectID { get; set; }       
        public Project Project { get; set; }

        //Collections
        public virtual ICollection<Staff> Staffs { get; set; }

        // Collection of materials
        public ICollection<Material> Materials { get; set; }
        public Material Material { get; set; }

        // Collection of labour
        public ICollection<Labour> Labours { get; set; }
        public Labour Labour { get; set; }
    }    
}
