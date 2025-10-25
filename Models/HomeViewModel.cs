using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JewelryGolden.Models
{
    public class HomeViewModel
    {
        public List<NewsCategory> NewsCategories { get; set; }
        public List<Product> products { get; set; }
         
        public List<ProductCategory> ProductCategories { get; set; }
    }
}