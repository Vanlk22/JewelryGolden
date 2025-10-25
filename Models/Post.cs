using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace JewelryGolden.Models
{


    [Table("Posts")]
    public class Post
    {
        [Key]
        public int ID { get; set; }

        [Required]
        [MaxLength(256)]
        public string Name { get; set; }

        [MaxLength(256)]
        public string Alias { get; set; }

        [Required]
        public int CategoryID { get; set; } // Khóa ngoại đến NewsCategory

        public string Image { get; set; }

        public string Description { get; set; } // Mô tả ngắn

        public string Content { get; set; } // Nội dung chi tiết

        public bool HomeFlag { get; set; }

        public bool HotFlag { get; set; }

        public int? ViewCount { get; set; }

        public bool Status { get; set; }

        // Thuộc tính điều hướng
        [ForeignKey("CategoryID")]
        public virtual NewsCategory NewsCategory { get; set; }
    }
}
