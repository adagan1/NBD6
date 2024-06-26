﻿using System.ComponentModel.DataAnnotations.Schema;
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

        [Display(Name = "First Name")]
        [Required(ErrorMessage = "You cannot leave the first name blank.")]
        [StringLength(50, ErrorMessage = "First name cannot be more than 50 characters long.")]
        [RegularExpression(@"^[a-zA-Z]+(?: [a-zA-Z]+)*$", ErrorMessage = "Client name can only contain letters.")]
        public string FirstName { get; set; }

        [Display(Name = "Middle Name")]
        [StringLength(50, ErrorMessage = "Middle name cannot be more than 50 characters long.")]
        [RegularExpression(@"^[a-zA-Z]+(?: [a-zA-Z]+)*$", ErrorMessage = "Client name can only contain letters.")]
        public string MiddleName { get; set; }

        [Display(Name = "Last Name")]
        [Required(ErrorMessage = "You cannot leave the last name blank.")]
        [StringLength(50, ErrorMessage = "Last name cannot be more than 50 characters long.")]
        [RegularExpression(@"^[a-zA-Z]+(?: [a-zA-Z]+)*$", ErrorMessage = "Client name can only contain letters.")]
        public string LastName { get; set; }

        [Display(Name = "Contact")]
        [Required(ErrorMessage = "You cannot leave the contact information blank.")]
        [StringLength(100, ErrorMessage = "Contact information cannot be more than 100 characters long.")]
        [RegularExpression(@"^\b[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Z|a-z]{2,}\b$", ErrorMessage = "Please enter a valid email address, including the '@' symbol.")]
        public string ClientContact { get; set; }


        [Display(Name = "Phone")]
        [Required(ErrorMessage = "You cannot leave the phone number blank.")]
        [RegularExpression(@"^\d{3}-\d{3}-\d{4}$", ErrorMessage = "Phone number must be in the format xxx-xxx-xxxx.")]
        [StringLength(12, MinimumLength = 12, ErrorMessage = "Phone number must be 12 characters long in the format xxx-xxx-xxxx.")]
        public string ClientPhone { get; set; }

        [Display(Name = "Client Name")]
        public string ClientName => $"{FirstName} {MiddleName} {LastName}";

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
                    return ClientPhone;
            }
        }
        public string ClientSummary
        {
            get
            {
                return $"{CompanyName} {ClientName}, {ClientContact}, {FormattedPhone}";
            }
        }

        public int AddressID { get; set; }
        public Address Address { get; set; }

        public virtual ICollection<Project> Projects { get; set; }
    }
}

