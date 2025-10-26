using JewelryGolden.Models;
using System;

namespace JewelryGolden.Migrations
{
    using JewelryGolden.Models;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<JewelryGolden.Models.JewelryDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(JewelryGolden.Models.JewelryDbContext context)
        {

            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            if (!context.Roles.Any(r => r.Name == "Admin"))
            {
                roleManager.Create(new IdentityRole("Admin"));
            }


            if (!context.Users.Any(u => u.UserName == "admin"))
            {
                var user = new ApplicationUser
                {
                    UserName = "admin",
                    Email = "admin@jewelry.com"

                };
                userManager.Create(user, "Abc@123");
                userManager.AddToRole(user.Id, "Admin");
            }



            if (!context.ProductCategories.Any())
            {
                var categories = new List<ProductCategory>
                {
                    new ProductCategory { Name = "Rings", Alias = "rings", Status = true },
                    new ProductCategory { Name = "Bracelets", Alias = "bracelets", Status = true },
                    new ProductCategory { Name = "Earrings", Alias = "earrings", Status = true },
                    new ProductCategory { Name = "Necklaces", Alias = "necklaces", Status = true },
                    new ProductCategory { Name = "Watchs", Alias = "watchs", Status = true }
                };
                context.ProductCategories.AddRange(categories);
                context.SaveChanges();
            }


            if (context.Products.Any())
            {

                var nhanCat = context.ProductCategories.FirstOrDefault(c => c.Alias == "rings");
                var dayChuyenCat = context.ProductCategories.FirstOrDefault(c => c.Alias == "bracelets");
                var bongTaiCat = context.ProductCategories.FirstOrDefault(c => c.Alias == "earrings");
                var vongTayCat = context.ProductCategories.FirstOrDefault(c => c.Alias == "necklaces");
                var dongHoCat = context.ProductCategories.FirstOrDefault(c => c.Alias == "watchs");

                var products = new List<Product>
                {

                    new Product
                    {
                        Name = "Una Angelic choker",
                        Alias = "una-angelic-choker",
                        CategoryID = dayChuyenCat.ID ,
                        Image = "/Resources/img/product-1.jpg",
                        MoreImages = null,
                        Price = 50.00M,             // Giá Bán (Đã giảm)
                        PromotionPrice = 50.00M,    // Giá Khuyến Mãi (Nếu Price là giá đã giảm)
                        OriginalPrice = 123.00M,    // Giá Gốc
                        Warranty = 12,              // Bảo hành 12 tháng
                        Description = "Elegant choker necklace, angel wing design.",
                        Content = null,
                        HomeFlag = true,            // Hiển thị trên trang chủ
                        HotFlag = true,             // Sản phẩm hot
                        ViewCount = 100,
                        Status = true,
                        CreatedDate = DateTime.Now
                    },

                        // 2. Stilla Attract stud earrings
                        new Product
                        {
                            Name = "Stilla Attract stud earrings",
                            Alias = "stilla-attract-stud-earrings",
                            CategoryID = bongTaiCat.ID,
                            Image = "/Resources/img/product-2.jpg",
                            MoreImages = null,
                            Price = 50.00M,
                            PromotionPrice = null,
                            OriginalPrice = 80.00M, // Sửa giá gốc hợp lý hơn so với giá bán 50.00M
                            Warranty = 6,
                            Description = "Sparkling Stilla Attract stud earrings.",
                            Content = null,
                            HomeFlag = true,
                            HotFlag = false,
                            ViewCount = 80,
                            Status = true,
                            CreatedDate = DateTime.Now
                        },

                        // 3. Ariana Grande Necklace
                        new Product
                        {
                            Name = "Ariana Grande Necklace",
                            Alias = "ariana-grande-necklace",
                            CategoryID = dayChuyenCat.ID,
                            Image = "/Resources/img/product-3.jpg",
                            MoreImages = null,
                            Price = 50.00M,
                            PromotionPrice = 50.00M,
                            OriginalPrice = 120.00M,
                            Warranty = 12,
                            Description = "Ariana Grande necklace is delicate and outstanding.",
                            Content = null,
                            HomeFlag = true,
                            HotFlag = true,
                            ViewCount = 150,
                            Status = true,
                            CreatedDate = DateTime.Now
                        },

                        // 4. Magic bracelet
                        new Product
                        {
                            Name = "Magic bracelet",
                            Alias = "magic-bracelet",
                            CategoryID = vongTayCat.ID,
                            Image = "/Resources/img/product-4.jpg",
                            MoreImages = null,
                            Price = 50.00M,
                            PromotionPrice = null,
                            OriginalPrice = 130.00M,
                            Warranty = 6,
                            Description = "Magic bracelet sparkles, unique design.",
                            Content = null,
                            HomeFlag = false,
                            HotFlag = true,
                            ViewCount = 90,
                            Status = true,
                            CreatedDate = DateTime.Now
                        },

                        // 5. Sublima layered pendant
                        new Product
                        {
                            Name = "Sublima layered pendant",
                            Alias = "sublima-layered-pendant",
                            CategoryID = dayChuyenCat.ID,
                            Image = "/Resources/img/product-5.jpg",
                            MoreImages = null,
                            Price = 50.00M,
                            PromotionPrice = null,
                            OriginalPrice = 85.00M, // Sửa giá gốc
                            Warranty = 12,
                            Description = "Luxurious, multi-layered Sublima pendant.",
                            Content = null,
                            HomeFlag = false,
                            HotFlag = false,
                            ViewCount = 50,
                            Status = true,
                            CreatedDate = DateTime.Now
                        },

                        // 6. Stilla Attract hoop earrings
                        new Product
                        {
                            Name = "Stilla Attract hoop earrings",
                            Alias = "stilla-attract-hoop-earrings",
                            CategoryID = bongTaiCat.ID,
                            Image = "/Resources/img/product-6.jpg",
                            MoreImages = null,
                            Price = 50.00M,
                            PromotionPrice = null,
                            OriginalPrice = 70.00M, // Sửa giá gốc
                            Warranty = 6,
                            Description = "Exquisite Stilla Attract Hoop Earrings.",
                            Content = null,
                            HomeFlag = true,
                            HotFlag = false,
                            ViewCount = 110,
                            Status = true,
                            CreatedDate = DateTime.Now
                        },

                        // 7. Watch (Đồng hồ)
                        new Product
                        {
                            Name = "Watch Ruby",
                            Alias = "watch-ruby",
                            CategoryID = dongHoCat.ID,
                            Image = "/Resources/img/product-7.jpg",
                            MoreImages = null,
                            Price = 50.00M,
                            PromotionPrice = 50.00M,
                            OriginalPrice = 120.00M,
                            Warranty = 24, // Đồng hồ thường bảo hành lâu hơn
                            Description = "Luxury watch, modern design.",
                            Content = null,
                            HomeFlag = true,
                            HotFlag = true,
                            ViewCount = 200,
                            Status = true,
                            CreatedDate = DateTime.Now
                        },

                        // 8. Magic pendant and brooch
                        new Product
                        {
                            Name = "Magic pendant and brooch",
                            Alias = "magic-pendant-and-brooch",
                            CategoryID = dayChuyenCat.ID,
                            Image = "/Resources/img/product-8.jpg",
                            MoreImages = null,
                            Price = 50.00M,
                            PromotionPrice = null,
                            OriginalPrice = 80.00M,
                            Warranty = 12,
                            Description = "Magic multi-purpose pendant and brooch.",
                            Content = null,
                            HomeFlag = false,
                            HotFlag = false,
                            ViewCount = 60,
                            Status = true,
                            CreatedDate = DateTime.Now
                        },

                        // 9. Stilla drop earrings
                        new Product
                        {
                            Name = "Stilla drop earrings",
                            Alias = "stilla-drop-earrings",
                            CategoryID = bongTaiCat.ID,
                            Image = "/Resources/img/product-10.jpg",
                            MoreImages = null,
                            Price = 50.00M,
                            PromotionPrice = null,
                            OriginalPrice = 80.00M,
                            Warranty = 6,
                            Description = "Stilla Attract delicate teardrop earrings.",
                            Content = null,
                            HomeFlag = true,
                            HotFlag = false,
                            ViewCount = 75,
                            Status = true,
                            CreatedDate = DateTime.Now
                        },

                        // 10. Millenia hoop earrings
                        new Product
                        {
                            Name = "Millenia hoop earrings",
                            Alias = "millenia-hoop-earrings",
                            CategoryID = bongTaiCat.ID,
                            Image = "/Resources/img/product-11.jpg",
                            MoreImages = null,
                            Price = 50.00M,
                            PromotionPrice = 50.00M,
                            OriginalPrice = 100.00M,
                            Warranty = 6,
                            Description = "Millenia sparkling hoop earrings.",
                            Content = null,
                            HomeFlag = true,
                            HotFlag = true,
                            ViewCount = 130,
                            Status = true,
                            CreatedDate = DateTime.Now
                        },

                        // 11. Gema hoop earrings
                        new Product
                        {
                            Name = "Gema hoop earrings",
                            Alias = "gema-hoop-earrings",
                            CategoryID = bongTaiCat.ID,
                            Image = "/Resources/img/product-12.jpg",
                            MoreImages = null,
                            Price = 50.00M,
                            PromotionPrice = null,
                            OriginalPrice = 75.00M,
                            Warranty = 6,
                            Description = "Gema modern style hoop earrings.",
                            Content = null,
                            HomeFlag = false,
                            HotFlag = true,
                            ViewCount = 95,
                            Status = true,
                            CreatedDate = DateTime.Now
                        }
                };
                context.Products.AddRange(products);
                context.SaveChanges();
            }
        }
    }
}
