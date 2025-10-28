using JewelryGolden.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace JewelryGolden
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            Database.SetInitializer<JewelryDbContext>(null);

        }
        private void CreateDefaultRolesAndUsers()
        {
            // JewelryDbContext là DbContext Identity của bạn
            var context = new JewelryDbContext();

            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));

            // 1️⃣ Tạo role Admin nếu chưa có
            if (!roleManager.RoleExists("Admin"))
            {
                var role = new IdentityRole("Admin");
                roleManager.Create(role);
            }

            // 2️⃣ Tạo role User nếu chưa có
            if (!roleManager.RoleExists("User"))
            {
                var role = new IdentityRole("User");
                roleManager.Create(role);
            }

            // 3️⃣ Gán tài khoản admin vào role Admin
            var adminUser = userManager.FindByEmail("admin@jewelry.com"); // đổi đúng email admin của bạn
            if (adminUser != null && !userManager.IsInRole(adminUser.Id, "Admin"))
            {
                userManager.AddToRole(adminUser.Id, "Admin");
            }

            // 4️⃣ Gán tài khoản khác vào role User
            var normalUser = userManager.FindByEmail("lekhacvan123@gmail.com");
            if (normalUser != null && !userManager.IsInRole(normalUser.Id, "User"))
            {
                userManager.AddToRole(normalUser.Id, "User");
            }
        }
    }
}
