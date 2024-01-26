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

        [Display(Name = "First Name")]
        [Required(ErrorMessage = "You cannot leave the first name blank.")]
        [StringLength(50, ErrorMessage = "First name cannot be more than 50 characters long.")]
        [RegularExpression(@"^[a-zA-Z]*$", ErrorMessage = "First name can only contain letters.")]
        public string ClientFirstName { get; set; }

        [Display(Name = "Last Name")]
        [Required(ErrorMessage = "You cannot leave the last name blank.")]
        [StringLength(50, ErrorMessage = "Last name cannot be more than 50 characters long.")]
        [RegularExpression(@"^[a-zA-Z]*$", ErrorMessage = "Last name can only contain letters.")]
        public string ClientLastName { get; set; }

        [Display(Name = "Contact")]
        [StringLength(100, ErrorMessage = "Contact information cannot be more than 100 characters long.")]
        public string ClientContact { get; set; }

        [Display(Name = "Phone")]
        [Phone(ErrorMessage = "The phone number is not in a valid format.")]
        [StringLength(20, ErrorMessage = "Phone number cannot be more than 20 characters long.")]
        [RegularExpression(@"^\d{10}$", ErrorMessage = "Phone number must be exactly 10 digits long.")]
        public string ClientPhone { get; set; }

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

