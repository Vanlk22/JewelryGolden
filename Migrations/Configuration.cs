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

            // Tạo role Admin nếu chưa có
            if (!roleManager.RoleExists("Admin"))
            {
                roleManager.Create(new IdentityRole("Admin"));
            }

            // Tạo tài khoản admin nếu chưa có
            if (!userManager.Users.Any(u => u.UserName == "admin@jewelry.com"))
            {
                var user = new ApplicationUser
                {
                    UserName = "admin@jewelry.com",
                    Email = "admin@jewelry.com",
                    EmailConfirmed = true
                };

                // ✅ Tạo user với hashing chuẩn
                var result = userManager.Create(user, "Abc@123");

                if (result.Succeeded)
                {
                    userManager.AddToRole(user.Id, "Admin");
                }
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
                        },
                         new Product
                        {
                            Name = "Ring Open",
                            Alias = "ring-open",
                            CategoryID = nhanCat.ID,
                            Image = "/Resources/img/product-13.jpg",
                            MoreImages = null,
                            Price = 70.00M,
                            PromotionPrice = null,
                            OriginalPrice = 80.00M, // Sửa giá gốc hợp lý hơn so với giá bán 50.00M
                            Warranty = 12,
                            Description = "This asymmetrical cocktail ring is sure to become a new favorite in your jewelry collection. The open gold-plated ring features purple crystals on one side and caramel crystals on the other, and is designed in a double-prong design. Wear it alone or combine it with other rings in the Millenia collection for the perfect modern elegance.",
                            Content = null,
                            HomeFlag = true,
                            HotFlag = false,
                            ViewCount = 70,
                            Status = true,
                            CreatedDate = DateTime.Now
                        },

                          new Product
                        {
                            Name = "Ring Gretc",
                            Alias = "ring-gretc",
                            CategoryID = nhanCat.ID,
                            Image = "/Resources/img/product-14.jpg",
                            MoreImages = null,
                            Price = 90.00M,
                            PromotionPrice = null,
                            OriginalPrice = 100.00M, // Sửa giá gốc hợp lý hơn so với giá bán 50.00M
                            Warranty = 12,
                            Description = "This hypnotic band ring offers a mix of colors and shapes. The overlapping band is rhodium-plated and features clear and green Swarovski Zirconia in round and baguette cuts. A vibrant choice that pairs beautifully with a matching bracelet and earrings.",
                            Content = null,
                            HomeFlag = true,
                            HotFlag = false,
                            ViewCount = 45,
                            Status = true,
                            CreatedDate = DateTime.Now
                        },

                           new Product
                        {
                            Name = "Ring Mul",
                            Alias = "ring-mul",
                            CategoryID = nhanCat.ID,
                            Image = "/Resources/img/product-15.jpg",
                            MoreImages = null,
                            Price = 65.00M,
                            PromotionPrice = null,
                            OriginalPrice = 80.00M, // Sửa giá gốc hợp lý hơn so với giá bán 50.00M
                            Warranty = 12,
                            Description = "Featuring a rainbow of vibrant colors, this Matrix ring is full of joy and elegance. The gold-plated band creates a full circle of baguette-cut stones, each set in a delicate prong setting. This jewelry is made for self-expression and pairs beautifully with our matching earrings.",
                            Content = null,
                            HomeFlag = true,
                            HotFlag = false,
                            ViewCount = 70,
                            Status = true,
                            CreatedDate = DateTime.Now
                        },
                            new Product
                        {
                            Name = "Ring Sq White",
                            Alias = "ring-sq-white",
                            CategoryID = nhanCat.ID,
                            Image = "/Resources/img/product-16.jpg",
                            MoreImages = null,
                            Price = 75.00M,
                            PromotionPrice = null,
                            OriginalPrice = 90.00M, // Sửa giá gốc hợp lý hơn so với giá bán 50.00M
                            Warranty = 12,
                            Description = "This attractive cocktail ring from the Stilla range is made to be noticed. Striking a balance between statement and sophistication, it features a delicate square-cut stone set in a double prong setting finished with pave stones and a silver-tone band.",
                            Content = null,
                            HomeFlag = true,
                            HotFlag = false,
                            ViewCount = 60,
                            Status = true,
                            CreatedDate = DateTime.Now
                        },
                                new Product
                        {
                            Name = "Bracelet Una",
                            Alias = "bracelet-una",
                             CategoryID = dayChuyenCat.ID,
                            Image = "/Resources/img/product-17.jpg",
                            MoreImages = null,
                            Price = 35.00M,
                            PromotionPrice = null,
                            OriginalPrice = 60.00M, // Sửa giá gốc hợp lý hơn so với giá bán 50.00M
                            Warranty = 6,
                            Description = "This innovative bracelet can be worn in two unique ways. The rhodium-plated chain has a sliding mechanism for an easy, adjustable fit and is decorated with metallic balls and a sparkling double-sided design. Simply invert the bracelet to showcase the different round-cut crystals. Mix and match with other Una Angelic products for the ultimate radiant look.",
                            Content = null,
                            HomeFlag = true,
                            HotFlag = false,
                            ViewCount = 120,
                            Status = true,
                            CreatedDate = DateTime.Now
                        },
                             new Product
                        {
                            Name = "Bracelet Matrix",
                            Alias = "bracelet-matrix",
                            CategoryID = vongTayCat.ID,
                            Image = "/Resources/img/product-18.jpg",
                            MoreImages = null,
                            Price = 70.00M,
                            PromotionPrice = null,
                            OriginalPrice = 130.00M,
                            Warranty = 6,
                            Description = "Colorful, dynamic, and expressive, this Matrix bracelet is a fun way to accessorize. With a spring-loaded hinge mechanism for easy opening, the overlapping band is gold-plated and features caramel and purple Swarovski Zirconia in square and baguette cuts. A vibrant choice that pairs beautifully with the matching ring.",
                            Content = null,
                            HomeFlag = false,
                            HotFlag = true,
                            ViewCount = 90,
                            Status = true,
                            CreatedDate = DateTime.Now
                        },
                              new Product
                        {
                            Name = "Bracelet Magr",
                            Alias = "bracelet-magr",
                            CategoryID = vongTayCat.ID,
                            Image = "/Resources/img/product-19.jpg",
                            MoreImages = null,
                            Price = 72.00M,
                            PromotionPrice = null,
                            OriginalPrice = 110.00M,
                            Warranty = 6,
                            Description = "With a dynamic combination of shapes, this Imber bracelet will bring a special glow to your wrist. The gold-plated chain features a lobster clasp and a series of metal drops, interspersed with round blue crystals in a delicate bezel. This bracelet also has extra stretch, allowing it to fit all sizes. Perfect for adding softness and grace to an outfit.",
                            Content = null,
                            HomeFlag = false,
                            HotFlag = true,
                            ViewCount = 150,
                            Status = true,
                            CreatedDate = DateTime.Now
                        },
                               new Product
                        {
                            Name = "Constella Bracelet",
                            Alias = "constella-bracelet",
                            CategoryID = vongTayCat.ID,
                            Image = "/Resources/img/product-20.jpg",
                            MoreImages = null,
                            Price = 100.00M,
                            PromotionPrice = null,
                            OriginalPrice = 130.00M,
                            Warranty = 6,
                            Description = "Wrap your wrist in light with this dazzling Constella bracelet. The rhodium-plated design features a full border of clear crystals and blue Swarovski Zirconia in a variety of sizes and shades, each presented in a pendant or hoop. Finished with a lobster clasp, this bracelet adds a sophisticated look to any outfit and can be worn with a matching necklace.",
                            Content = null,
                            HomeFlag = false,
                            HotFlag = true,
                            ViewCount = 200,
                            Status = true,
                            CreatedDate = DateTime.Now
                        },
                        new Product
                        {
                            Name = "Matrix Clock",
                            Alias = "matrix-clock",
                            CategoryID = dongHoCat.ID,
                            Image = "/Resources/img/product-21.jpg",
                            MoreImages = null,
                            Price = 150.00M,
                            PromotionPrice = null,
                            OriginalPrice = 120.00M,
                            Warranty = 24, // Đ1ồng hồ thường bảo hành lâu hơn
                            Description = "Like a sparkling jewel on the wrist, the Matrix Pearl Bangle is the ultimate timepiece. The 26mm stainless steel design is finished in rose gold and set with 33 pavé crystals on the bezel. The sunburst dial is accented with additional crystals for the hour markers and the swan logo at 12 o’clock. For a truly stunning effect, the bracelet is intricately decorated with 152 clear pavé crystals and a central row of Swarovski pearls. This Swiss timepiece is water-resistant to 50 meters and can be paired with jewelry from the Matrix line for a luxurious look.",
                            Content = null,
                            HomeFlag = true,
                            HotFlag = true,
                            ViewCount = 1200,
                            Status = true,
                            CreatedDate = DateTime.Now
                        }, new Product
                        {
                            Name = "Matrix Octagon Watch",
                            Alias = "matrix-octagon-watch",
                            CategoryID = dongHoCat.ID,
                            Image = "/Resources/img/product-22.jpg",
                            MoreImages = null,
                            Price = 125.00M,
                             PromotionPrice = null,
                            OriginalPrice = 140.00M,
                            Warranty = 24, // Đồng hồ thường bảo hành lâu hơn
                            Description = "This innovative timepiece is a one-of-a-kind creation that combines distinctive design with a mastery of light. Crafted with true craftsmanship, the timepiece is inspired by the Matrix jewelry family and features a 13 x 18 mm stainless steel case with a champagne gold-tone finish. The bezel is presented in the House’s signature octagonal shape and is beautifully set with a combination of 78 round and fancy-cut crystals and Swarovski Zirconia. This forms a radiant frame for the minimalist sunray dial, which elegantly displays three crystal hour markers and the swan logo. A single crystal is also set inside the crown, while around the wrist, the elegance is further enhanced by an adjustable mesh bracelet. The timepiece is water-resistant to 50m and represents Swarovski’s modern luxury at its best.",
                            Content = null,
                            HomeFlag = true,
                            HotFlag = true,
                            ViewCount = 1200,
                            Status = true,
                            CreatedDate = DateTime.Now
                        }, new Product
                        {
                            Name = "Imber Oval Watch",
                            Alias = "imber-oval-watch",
                            CategoryID = dongHoCat.ID,
                            Image = "/Resources/img/product-23.jpg",
                            MoreImages = null,
                            Price = 145.00M,
                            PromotionPrice = null,
                            OriginalPrice = 160.00M,
                            Warranty = 24, // Đồng hồ thường bảo hành lâu hơn
                            Description = "The Imber Oval conveys a modern approach to watchmaking through its sparkling dial and timeless ellipse. This 24 x 26.3 mm stainless steel version is finished in champagne gold and features approximately 1,000 clear crystals on the Crystalline dial. Among the slim hour markers, a single Roman numeral is displayed at 6 o’clock, while Swarovski’s iconic swan is positioned at 12 o’clock. The Swiss-made timepiece is worn on a blue crocodile-patterned leather strap and is water-resistant to 50m. Make this your go-to choice for sophisticated day-to-night style.",
                            Content = null,
                            HomeFlag = true,
                            HotFlag = true,
                            ViewCount = 2200,
                            Status = true,
                            CreatedDate = DateTime.Now
                        }, new Product
                        {
                            Name = "Dextera Watch",
                            Alias = "dextera-watch",
                            CategoryID = dongHoCat.ID,
                            Image = "/Resources/img/product-24.jpg",
                            MoreImages = null,
                            Price = 140.00M,
                            PromotionPrice = null,
                            OriginalPrice = 160.00M,
                            Warranty = 24, // Đồng hồ thường bảo hành lâu hơn
                            Description = "This charming Dextera watch is inspired by classic timepieces from the classical era. The 29mm stainless steel design is creatively decorated with a layer of transparent crystal on half the bezel and half the bracelet. The minimalist dial has a sunburst effect and is punctuated only by three crystal hour markers and a swan logo at 12 o’clock. A total of 203 crystals are used to create this watch, including a single crystal set into the crown. This Swiss-made timepiece is water-resistant to 50 meters and is a pure expression of Swarovski sophistication.",
                            Content = null,
                            HomeFlag = true,
                            HotFlag = true,
                            ViewCount = 1200,
                            Status = true,
                            CreatedDate = DateTime.Now
                        }, new Product
                        {
                            Name = "Millenia Watches",
                            Alias = "millenia-watchs",
                            CategoryID = dongHoCat.ID,
                            Image = "/Resources/img/product-25.jpg",
                            MoreImages = null,
                            Price = 76.00M,
                            PromotionPrice = null,
                            OriginalPrice = 120.00M,
                            Warranty = 24, // Đồng hồ thường bảo hành lâu hơn
                            Description = "Luxury watch, modern design.",
                            Content = null,
                            HomeFlag = true,
                            HotFlag = true,
                            ViewCount = 2200,
                            Status = true,
                            CreatedDate = DateTime.Now
                        },

                };
    context.Products.AddRange(products);
                context.SaveChanges();
            }
        }
    }
}
