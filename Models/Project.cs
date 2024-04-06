using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NBD6.Models
{
    public class Project : IValidatableObject
    {
        [Key]
        [Display(Name = "Project ID")]
        public int ProjectID { get; set; }

        [Display(Name = "Project Name")]
        [Required(ErrorMessage = "You cannot leave the project name blank.")]
        [StringLength(200, ErrorMessage = "Project name cannot be more than 200 characters long.")]
        [RegularExpression(@"^[a-zA-Z0-9\s]*$", ErrorMessage = "Project name can only contain letters and numbers.")]
        public string ProjectName { get; set; }

        [Display(Name = "Start Date")]
        [DataType(DataType.Date)]
        [Required(ErrorMessage = "You must enter a start date.")]
        public DateTime ProjectStartDate { get; set; }

        [Display(Name = "End Date")]
        [DataType(DataType.Date)]
        [Required(ErrorMessage = "You must enter an end date.")]
        public DateTime ProjectEndDate { get; set; }

        [Display(Name = "Project Site")]
        [Required(ErrorMessage = "Project site is required.")]
        [StringLength(200, ErrorMessage = "Project site cannot be more than 200 characters long.")]
        public string ProjectSite { get; set; }

        [Display(Name = "Bid Amount")]
        [Required(ErrorMessage = "Bid amount is required")]
        [StringLength(200, ErrorMessage = "Bid amount can only contain positive numbers.")]
        [RegularExpression(@"^\d*\.?\d+$", ErrorMessage = "Bid amount must be a positive number.")]
        public string BidAmount { get; set; }

        public string ProjectSummary
        {
            get
            {
                return $"{ProjectName} {ProjectSite}, Start Date: {ProjectStartDate}, End Date: {ProjectEndDate}";
            }
        }

        //FK
        [Display(Name = "Client ID")]
        [ForeignKey("Client")]
        [Required(ErrorMessage = "You must select a client.")]

        public int ClientID { get; set; }
        public Client Client { get; set; }

        [Display(Name = "Address ID")]
        [ForeignKey("Address")]
        [Required(ErrorMessage = "You must select an address.")]

        public int AddressID { get; set; }
        public Address Address { get; set; }

        public virtual ICollection<Bid> Bids { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            // Check if the end date is before the start date
            if (ProjectEndDate < ProjectStartDate)
            {
                yield return new ValidationResult("Project cannot end before it starts.", new[] { "ProjectEndDate" });
            }
        }
    }
}

