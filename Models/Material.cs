﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NBD6.Models
{
    public class Material
    {
        public int MaterialID { get; set; }

        //Material fields
        [Display(Name = "Type")]
        [Required(ErrorMessage = "Material type is required.")]
        public string MaterialType { get; set; }

        [Display(Name = "Quantity")]
        [RegularExpression(@"^(?!-)\d+(\.\d{1,2})?$", ErrorMessage = "Quantity must be a valid number.")]
        public int MaterialQuantity { get; set; }

        [Display(Name = "Description")]
        [RegularExpression(@"^[a-zA-Z0-9\s]+$")]
        public string MaterialDescription { get; set; }

        [Display(Name = "Size")]
        [Required(ErrorMessage = "Please provide a unit of measurement after the digit (cm, m, l, g, kg, gal, yd, in)")]
        [RegularExpression(@"^(?:\d*\.?\d+)\s*(cm|m|l|g|kg|cubic\s*cm|cubic\s*m|gal|yd|in)$")]

        public string MaterialSize { get; set; }

        [Display(Name = "Price")]
        [RegularExpression(@"^(?!-)\d+(\.\d{1,2})?$", ErrorMessage = "Unit Price must be a valid number.")]
        public decimal MaterialPrice { get; set; }

        public decimal ExtendedMaterialPrice => MaterialQuantity * MaterialPrice;

        //FK
        public int? BidID { get; set; }
        public Bid? Bid { get; set; }
    }
}