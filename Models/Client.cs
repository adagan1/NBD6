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
        // Read-only property to get formatted phone number
        public string FormattedPhone
        {
            get
            {
                if (string.IsNullOrEmpty(ClientPhone))
                    return "";

                // Remove any non-numeric characters before formatting
                string digits = new String(ClientPhone.Where(char.IsDigit).ToArray());
                if (digits.Length == 10)
                    return $"{digits.Substring(0, 3)}-{digits.Substring(3, 3)}-{digits.Substring(6)}";
                else
                    return ClientPhone; // Return as is if not exactly 10 digits
            }
        }
        public string ClientSummary
        {
            get
            {
                return $"{CompanyName} {ClientName}, {ClientContact}, {FormattedPhone}";
            }
        }

        // Foreign Keys
        [Display(Name = "Address ID")]
        [ForeignKey("Address")]
        [Required(ErrorMessage = "Address ID is required.")]
        [Range(1, int.MaxValue, ErrorMessage = "Address ID must be a positive integer.")]
        public int AddressID { get; set; }
        public Address Address { get; set; } // Navigation property to future address class

        public virtual ICollection<Project> Projects { get; set; }
    }
}

