using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Services.Description;
namespace JewelryGolden.Models
{


    [Table("NewsCategories")]
    public class NewsCategory
    {
        [Key]
        public int ID { get; set; }

        [Required]
        [MaxLength(256)]
        public string Name { get; set; }

        [MaxLength(256)]
        public string Alias { get; set; }

        public string Description { get; set; }

        public int? DisplayOrder { get; set; }

        public bool Status { get; set; }

        // Thuộc tính điều hướng
        public virtual IEnumerable<Post> Posts { get; set; }
    }
}
