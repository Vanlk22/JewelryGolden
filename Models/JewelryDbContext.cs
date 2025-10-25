using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace JewelryGolden.Models
{
    public class JewelryDbContext : IdentityDbContext<ApplicationUser>
    {
        // Tên của Connection String trong file Web.config
        public JewelryDbContext() : base("JewelryConnection", throwIfV1Schema: false)
        {
            // Tắt Lazy Loading để tối ưu hiệu suất (tuỳ chọn)
            this.Configuration.LazyLoadingEnabled = false;
        }

        // Khai báo các DbSet tương ứng với các bảng trong CSDL
        public DbSet<ProductCategory> ProductCategories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<NewsCategory> NewsCategories { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
        public DbSet<Slide> Slides { get; set; }


        // Ghi đè phương thức OnModelCreating để tùy chỉnh ánh xạ (ít dùng cho mô hình cơ bản)
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            // Thiết lập khóa chính kép cho OrderDetail
            modelBuilder.Entity<OrderDetail>().HasKey(od => new { od.OrderID, od.ProductID });
            base.OnModelCreating(modelBuilder);

            // Tùy chọn: Đổi tên bảng Identity nếu cần (nên làm để giữ gọn gàng)
            modelBuilder.Entity<ApplicationUser>().ToTable("Users");
        }
    }
}