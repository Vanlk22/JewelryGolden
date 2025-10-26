using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace JewelryGolden.Models
{


    [Table("Products")]
    public class Product
    {
        [Key]
        public int ID { get; set; }

        [Required]
        [MaxLength(256)]
        public string Name { get; set; }

        [MaxLength(256)]
        public string Alias { get; set; }

        [Required]
        public int CategoryID { get; set; } // Khóa ngoại đến ProductCategory

        public string Image { get; set; }

        [Column(TypeName = "xml")]
        public string MoreImages { get; set; } // Lưu trữ nhiều hình ảnh dưới dạng XML/JSON

        public decimal Price { get; set; }

        public decimal? PromotionPrice { get; set; } // Giá khuyến mãi (nullable)

        public int? Warranty { get; set; } // Thời gian bảo hành

        public string Description { get; set; }

        public string Content { get; set; }

        public bool HomeFlag { get; set; } // Hiển thị trên trang chủ

        public bool HotFlag { get; set; } // Sản phẩm hot

        public int? ViewCount { get; set; } // Số lượt xem

        public bool Status { get; set; }

        // Thuộc tính điều hướng
        [ForeignKey("CategoryID")]
        public virtual ProductCategory ProductCategory { get; set; }

        // Thêm trường cơ bản cho mọi bảng (nếu cần thiết cho việc theo dõi)
        public System.DateTime? CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public System.DateTime? UpdatedDate { get; set; }
        [Required]
        public decimal OriginalPrice { get; set; }
        public string UpdatedBy { get; set; }
    }
}
