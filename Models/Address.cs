using System.ComponentModel.DataAnnotations;
using System.Composition;

namespace NBD6.Models
{
    public class Address
    {
        [Key]
        public int AddressID { get; set; }

        [Required]
        [MaxLength(100)]
        public string Country { get; set; }

        [Display(Name = "Province")]
        [Required]
        [MaxLength(100)]
        public string Province { get; set; }

        [Display(Name = "City")]
        [Required]
        [MaxLength(100)]
        public string City { get; set; }

        [Display(Name = "Postal Code")]
        [Required(ErrorMessage = "Postal code is required.")]
        [MaxLength(7, ErrorMessage = "Postal code cannot be more than 20 characters long.")]
        [RegularExpression(@"^[A-Za-z]\d[A-Za-z] [ -]?\d[A-Za-z]\d$", ErrorMessage = "Postal code must be in the format like L2G 7L3, with a space.")]
        public string Postal { get; set; }

        [Required]
        [MaxLength(200)]
        [RegularExpression(@"^\d+\s[A-Za-z]+\s*(\w*\s*)*$", ErrorMessage = "Please enter the street in the format 'number streetname'")]
        public string Street { get; set; }

        public int? ClientID { get; set; }
        public Client? Client { get; set; }

        public int? ProjectID { get; set; }
        public Project? Project { get; set; }
    }
}
