using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ABCSupermarketMVC.Models
{
    public partial class Product
    {
        public int ProductId { get; set; }
        [Required(ErrorMessage="Please fill in product name")]
        public string ProductName { get; set; }
        [Required(ErrorMessage = "Please fill in product description")]
        public string ProductDesc { get; set; }

        public byte[] ProductImage { get; set; }
        [Required(ErrorMessage = "Please fill in product price")]
        //[DataType(DataType.Currency, ErrorMessage ="Please enter valid decimal price")]
        public decimal? ProductPrice { get; set; }
    }
}
