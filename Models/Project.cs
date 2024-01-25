using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace NBD6.Models
{
    public class Project
    {
        [Key]
        [Display(Name = "Project ID")]
        public int ProjectID { get; set; }

        [Display(Name = "Project Name")]
        [Required(ErrorMessage = "You cannot leave the project name blank.")]
        [StringLength(200, ErrorMessage = "Project name cannot be more than 200 characters long.")]
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
        [StringLength(200, ErrorMessage = "Project site cannot be more than 200 characters long.")]
        public string ProjectSite { get; set; }

        [Display(Name = "Bid Amount")]
        [DataType(DataType.Currency)]
        [Required(ErrorMessage = "You must enter a bid amount.")]
        public decimal BidAmount { get; set; }


        //Foreign Keys
        [Display(Name = "Client ID")]
        [ForeignKey("Client")]
        [Required(ErrorMessage = "You must select a client.")]
        public int ClientID { get; set; }
        public Client Client { get; set; } // Navigation property to the Client class
    }
}
