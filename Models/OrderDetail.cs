using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace JewelryGolden.Models
{


    [Table("OrderDetails")]
    public class OrderDetail
    {
        [Key]
        [Column(Order = 1)]
        public int OrderID { get; set; } // Khóa ngoại đến Order

        [Key]
        [Column(Order = 2)]
        public int ProductID { get; set; } // Khóa ngoại đến Product

        [Required]
        public int Quantity { get; set; } // Số lượng sản phẩm

        // Thuộc tính điều hướng
        [ForeignKey("OrderID")]
        public virtual Order Order { get; set; }

        [ForeignKey("ProductID")]
        public virtual Product Product { get; set; }
    }
}
