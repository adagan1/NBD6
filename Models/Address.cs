using System.ComponentModel.DataAnnotations;

namespace NBD6.Models
{
    public class Address
    {
        [Key]
        public int AddressID { get; set; }  // Assuming AddressID is an integer primary key

        [Required]
        [MaxLength(100)] // Assuming a max length of 100 for string fields
        public string Country { get; set; }

        [Display(Name = "Province/State")]
        [Required]
        [MaxLength(100)]
        public string ProvinceState { get; set; }

        [Required]
        [MaxLength(20)] // Assuming AreaCode is a shorter string
        public string AreaCode { get; set; }

        [Required]
        [MaxLength(200)]
        public string Street { get; set; }

        public string AddressSummary { get; set; }
    }
}
