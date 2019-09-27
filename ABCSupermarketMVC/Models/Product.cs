using System;
using System.Collections.Generic;

namespace ABCSupermarketMVC.Models
{
    public partial class Product
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string ProductDesc { get; set; }
        public byte[] ProductImage { get; set; }
        public decimal? ProductPrice { get; set; }
    }
}
