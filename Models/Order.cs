using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace JewelryGolden.Models
{



    [Table("Orders")]
    public class Order
    {
        [Key]
        public int ID { get; set; }

        [Required]
        [MaxLength(256)]
        public string CustomerName { get; set; }

        [Required]
        [MaxLength(256)]
        public string CustomerAddress { get; set; }

        [Required]
        [MaxLength(50)]
        public string CustomerMobile { get; set; }

        [Required]
        [MaxLength(256)]
        public string CustomerEmail { get; set; }

        public string CustomerMessage { get; set; }

        public System.DateTime? CreatedDate { get; set; }

        public string PaymentMethod { get; set; } // Phương thức thanh toán

        public bool PaymentStatus { get; set; } // Trạng thái thanh toán

        public bool Status { get; set; } // Trạng thái đơn hàng (Mới, Đang giao, Hoàn tất...)

        // Thuộc tính điều hướng
        public virtual IEnumerable<OrderDetail> OrderDetails { get; set; }
    }
}
