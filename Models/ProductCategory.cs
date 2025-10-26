using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace JewelryGolden.Models
{
    [Table("ProductCategories")]
    public class ProductCategory
    {
        [Key]
        public int ID { get; set; }

        [Required]
        [MaxLength(256)]
        public string Name { get; set; }

        [MaxLength(256)]
        public string Alias { get; set; } // Dùng cho URL thân thiện

        public string Description { get; set; }

        public int? ParentID { get; set; } // Danh mục cha (nếu có)

        public int? DisplayOrder { get; set; }

        public bool Status { get; set; } // Trạng thái hiển thị (true/false)

        public string Image { get; set; } // Hình ảnh đại diện

        // Thuộc tính điều hướng (Navigation Property)
        [ForeignKey("CategoryID")]
        public virtual IEnumerable<Product> Products { get; set; }
    }
}
