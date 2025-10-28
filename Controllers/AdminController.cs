using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using JewelryGolden.Models;

namespace JewelryGolden.Areas.Admin.Controllers
{
    public class AdminController : Controller
    {
        private JewelryDbContext db = new JewelryDbContext();

        // GET: Admin
        public ActionResult Index()
        {
            // Lấy thống kê tổng quan
            ViewBag.TotalProducts = db.Products.Count();
            ViewBag.TotalOrders = db.Orders.Count();
            ViewBag.TotalCategories = db.ProductCategories.Count();
            ViewBag.TotalPosts = db.Posts.Count();
            
            return View();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
