using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using JewelryGolden.Models;

namespace JewelryGolden.Controllers
{
    public class ProductsController : Controller
    {
        private JewelryDbContext db = new JewelryDbContext();

       
        // GET: Products
        public ActionResult Index(int? categoryId)
        {
            // Lấy tất cả sản phẩm active
            var products = db.Products.Include(p => p.ProductCategory)
                            .Where(p => p.Status == true);

            // Filter theo category nếu có categoryId
            if (categoryId.HasValue)
            {
                products = products.Where(p => p.CategoryID == categoryId.Value);
                ViewBag.CurrentCategoryId = categoryId.Value;
                ViewBag.CategoryName = db.ProductCategories.Find(categoryId.Value)?.Name;
            }

            // Lấy danh sách categories cho sidebar (nếu cần)
            ViewBag.Categories = db.ProductCategories
                .Where(c => c.Status == true)
                .OrderBy(c => c.DisplayOrder)
                .ToList();

            return View(products.ToList());
        }
        
        public ActionResult Details(int? id)
        {
            // BƯỚC 1: Xử lý trường hợp ID bị thiếu (URL: /Products/Details)
            if (id == null)
            {
                // Trả về 404 Not Found (hợp lý hơn 400 trong trường hợp này)
                return HttpNotFound();
                // Hoặc: return RedirectToAction("Index"); để chuyển hướng về trang danh sách
            }

            // BƯỚC 2: Tìm sản phẩm trong CSDL
            Product product = db.Products.Find(id); // Giả sử db là ApplicationDbContext

            // BƯỚC 3: Xử lý trường hợp không tìm thấy sản phẩm với ID đã cho
            if (product == null)
            {
                return HttpNotFound();
            }

            // BƯỚC 4: Trả về View cùng với đối tượng Product
            return View(product);
        }
        //// GET: Products/Details/5
        //public ActionResult Details(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Product product = db.Products.Find(id);
        //    if (product == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(product);
        //}

        //// GET: Products/Create
        //public ActionResult Create()
        //{
        //    ViewBag.CategoryID = new SelectList(db.ProductCategories, "ID", "Name");
        //    return View();
        //}

        //// POST: Products/Create
        //// To protect from overposting attacks, enable the specific properties you want to bind to, for 
        //// more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create([Bind(Include = "ID,Name,Alias,CategoryID,Image,MoreImages,Price,PromotionPrice,Warranty,Description,Content,HomeFlag,HotFlag,ViewCount,Status,CreatedDate,CreatedBy,UpdatedDate,UpdatedBy")] Product product)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Products.Add(product);
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }

        //    ViewBag.CategoryID = new SelectList(db.ProductCategories, "ID", "Name", product.CategoryID);
        //    return View(product);
        //}

        //// GET: Products/Edit/5
        //public ActionResult Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Product product = db.Products.Find(id);
        //    if (product == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    ViewBag.CategoryID = new SelectList(db.ProductCategories, "ID", "Name", product.CategoryID);
        //    return View(product);
        //}

        //// POST: Products/Edit/5
        //// To protect from overposting attacks, enable the specific properties you want to bind to, for 
        //// more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit([Bind(Include = "ID,Name,Alias,CategoryID,Image,MoreImages,Price,PromotionPrice,Warranty,Description,Content,HomeFlag,HotFlag,ViewCount,Status,CreatedDate,CreatedBy,UpdatedDate,UpdatedBy")] Product product)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Entry(product).State = EntityState.Modified;
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }
        //    ViewBag.CategoryID = new SelectList(db.ProductCategories, "ID", "Name", product.CategoryID);
        //    return View(product);
        //}

        //// GET: Products/Delete/5
        //public ActionResult Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Product product = db.Products.Find(id);
        //    if (product == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(product);
        //}

        //// POST: Products/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(int id)
        //{
        //    Product product = db.Products.Find(id);
        //    db.Products.Remove(product);
        //    db.SaveChanges();
        //    return RedirectToAction("Index");
        //}

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
