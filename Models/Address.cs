using System.ComponentModel.DataAnnotations;
using System.Composition;

namespace NBD6.Models
{
    public class Address
    {
        [Key]
        public int AddressID { get; set; }  // Assuming AddressID is an integer primary key

        [Required]
        [MaxLength(100)] // Assuming a max length of 100 for string fields
        public string Country { get; set; }

        [Display(Name = "Province")]
        [Required]
        [MaxLength(100)]
        public string Province { get; set; }

        [Display(Name = "Postal Code")]
        [Required(ErrorMessage = "Postal code is required.")]
        [MaxLength(20, ErrorMessage = "Postal code cannot be more than 20 characters long.")]
        [RegularExpression(@"^[A-Za-z]\d[A-Za-z][ -]?\d[A-Za-z]\d$", ErrorMessage = "Postal code must be in the formatted like L2G 7L3, with or without a space.")]
        public string Postal { get; set; }

        [Required]
        [MaxLength(200)]
        public string Street { get; set; }

        public int? ClientID { get; set; }
        public Client? Client { get; set; }

        public int? ProjectID { get; set; }
        public Project? Project { get; set; }

        public string AddressSummary
        {
            get
            {
                return $"{Country}, {Province}, {Street}, {Postal}";
            }
        }
    }
}
