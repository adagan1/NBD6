using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Net;

namespace NBD6.Models
{
    public class Client
    {
        [Key]
        [Display(Name = "Client ID")]
        public int ClientID { get; set; }

        [Display(Name = "Company")]
        [Required(ErrorMessage = "You cannot leave the Company's name blank.")]
        [StringLength(50, ErrorMessage = "The Company's name cannot be more than 50 characters long.")]
        [RegularExpression(@"^[a-zA-Z]+(?: [a-zA-Z]+)*$", ErrorMessage = "The Company's name can only contain letters.")]
        public string CompanyName { get; set; }

        [Display(Name = "Client Name")]
        [Required(ErrorMessage = "You cannot leave the Client name blank.")]
        [StringLength(50, ErrorMessage = "Client name cannot be more than 50 characters long.")]
        [RegularExpression(@"^[a-zA-Z]+(?: [a-zA-Z]+)*$", ErrorMessage = "Client name can only contain letters.")]
        public string ClientName { get; set; }

        [Display(Name = "Contact")]
        [StringLength(100, ErrorMessage = "Contact information cannot be more than 100 characters long.")]
        [RegularExpression(@"^\b[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Z|a-z]{2,}\b$", ErrorMessage = "Please enter a valid email address.")]
        public string ClientContact { get; set; }

        [Display(Name = "Phone")]
        [Phone(ErrorMessage = "The phone number is not in a valid format.")]
        [StringLength(20, ErrorMessage = "Phone number cannot be more than 20 characters long.")]
        [RegularExpression(@"^\d{3}-?\d{3}-?\d{4}$", ErrorMessage = "Phone number must be in the format xxx-xxx-xxxx or xxxxxxxxxx.")]
        public string ClientPhone { get; set; }

        public string ClientSummary
        {
            get
            {
                return $"{CompanyName} {ClientName}, {ClientContact}, {ClientPhone}";
            }
        }

        // Foreign Keys
        [Display(Name = "Address ID")]
        [ForeignKey("Address")]
        [Required(ErrorMessage = "Address ID is required.")]
        [Range(1, int.MaxValue, ErrorMessage = "Address ID must be a positive integer.")]
        public int AddressID { get; set; }
        public Address Address { get; set; } // Navigation property to future address class
        public virtual ICollection<Address> Addresses { get; set; } = new HashSet<Address>();
        public virtual ICollection<Project> Projects { get; set; }
    }
}