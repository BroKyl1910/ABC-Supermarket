using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ABCSupermarketMVC.Models
{
    public partial class Product
    {
        public int ProductId { get; set; }

        [Required(ErrorMessage="Please fill in product name")]
        [MaxLength(100, ErrorMessage ="Product name cannot exceed 100 characters")]
        public string ProductName { get; set; }

        [Required(ErrorMessage = "Please fill in product description")]
        [MaxLength(200, ErrorMessage = "Product description cannot exceed 200 characters")]
        public string ProductDesc { get; set; }

        public byte[] ProductImage { get; set; }

        [Required(ErrorMessage = "Please fill in product price")]
        [DisplayName("product price")]
        public decimal? ProductPrice { get; set; }
    }
}
