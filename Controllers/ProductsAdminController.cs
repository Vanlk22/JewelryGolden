using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using JewelryGolden.Models;
using System.IO;
using System.Globalization;

namespace JewelryGolden.Areas.Admin.Controllers
{
    public class ProductsAdminController : Controller
    {
        private JewelryDbContext db = new JewelryDbContext();

        // GET: Products
        public ActionResult Index()
        {
            var products = db.Products.Include(p => p.ProductCategory);
            return View(products.ToList());
        }

        // GET: Products/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // GET: Products/Create
        public ActionResult Create()
        {
            ViewBag.CategoryID = new SelectList(db.ProductCategories, "ID", "Name");
            return View();
        }

        // POST: Products/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Name,Alias,CategoryID,Image,MoreImages,OriginalPrice,Price,PromotionPrice,Warranty,Description,Content,HomeFlag,HotFlag,ViewCount,Status,CreatedDate,CreatedBy,UpdatedDate,UpdatedBy")] Product product, HttpPostedFileBase ImageFile)
        {
            // Normalize decimal inputs to avoid culture parsing issues
            NormalizeDecimalField("OriginalPrice", v => product.OriginalPrice = v);
            NormalizeDecimalField("Price", v => product.Price = v);
            NormalizeDecimalFieldNullable("PromotionPrice", v => product.PromotionPrice = v);

            // Set CreatedDate server-side
            product.CreatedDate = DateTime.Now;
            if (ModelState.ContainsKey("CreatedDate"))
            {
                ModelState["CreatedDate"].Errors.Clear();
            }

            if (ModelState.IsValid)
            {
                // Handle image upload if provided
                if (ImageFile != null && ImageFile.ContentLength > 0)
                {
                    var uploadsFolderVirtual = "/Resources/img/";
                    var uploadsFolderPhysical = Server.MapPath("~" + uploadsFolderVirtual);
                    if (!Directory.Exists(uploadsFolderPhysical))
                    {
                        Directory.CreateDirectory(uploadsFolderPhysical);
                    }

                    var fileExtension = Path.GetExtension(ImageFile.FileName);
                    var fileName = Guid.NewGuid().ToString("N") + fileExtension;
                    var savePath = Path.Combine(uploadsFolderPhysical, fileName);
                    ImageFile.SaveAs(savePath);
                    product.Image = uploadsFolderVirtual + fileName;
                }

                db.Products.Add(product);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CategoryID = new SelectList(db.ProductCategories, "ID", "Name", product.CategoryID);
            return View(product);
        }

        // GET: Products/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            ViewBag.CategoryID = new SelectList(db.ProductCategories, "ID", "Name", product.CategoryID);
            return View(product);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Name,Alias,CategoryID,Image,MoreImages,OriginalPrice,Price,PromotionPrice,Warranty,Description,Content,HomeFlag,HotFlag,ViewCount,Status,CreatedDate,CreatedBy,UpdatedDate,UpdatedBy")] Product product, HttpPostedFileBase ImageFile)
        {
            // Normalize decimal inputs to avoid culture parsing issues
            NormalizeDecimalField("OriginalPrice", v => product.OriginalPrice = v);
            NormalizeDecimalField("Price", v => product.Price = v);
            NormalizeDecimalFieldNullable("PromotionPrice", v => product.PromotionPrice = v);

            // Set UpdatedDate server-side
            product.UpdatedDate = DateTime.Now;
            if (ModelState.ContainsKey("UpdatedDate"))
            {
                ModelState["UpdatedDate"].Errors.Clear();
            }

            if (ModelState.IsValid)
            {
                // If a new image is uploaded, replace the current one
                if (ImageFile != null && ImageFile.ContentLength > 0)
                {
                    var uploadsFolderVirtual = "/Resources/img/";
                    var uploadsFolderPhysical = Server.MapPath("~" + uploadsFolderVirtual);
                    if (!Directory.Exists(uploadsFolderPhysical))
                    {
                        Directory.CreateDirectory(uploadsFolderPhysical);
                    }

                    var fileExtension = Path.GetExtension(ImageFile.FileName);
                    var fileName = Guid.NewGuid().ToString("N") + fileExtension;
                    var savePath = Path.Combine(uploadsFolderPhysical, fileName);
                    ImageFile.SaveAs(savePath);
                    product.Image = uploadsFolderVirtual + fileName;
                }

                db.Entry(product).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CategoryID = new SelectList(db.ProductCategories, "ID", "Name", product.CategoryID);
            return View(product);
        }

        private void NormalizeDecimalField(string key, Action<decimal> apply)
        {
            var raw = Request[key];
            if (!string.IsNullOrWhiteSpace(raw))
            {
                var normalized = raw.Replace(" ", string.Empty).Replace(".", string.Empty).Replace(",", string.Empty);
                decimal value;
                if (decimal.TryParse(normalized, NumberStyles.Number, CultureInfo.InvariantCulture, out value))
                {
                    apply(value);
                    if (ModelState.ContainsKey(key))
                    {
                        ModelState[key].Errors.Clear();
                    }
                }
            }
        }

        private void NormalizeDecimalFieldNullable(string key, Action<decimal?> apply)
        {
            var raw = Request[key];
            if (string.IsNullOrWhiteSpace(raw))
            {
                apply(null);
                if (ModelState.ContainsKey(key))
                {
                    ModelState[key].Errors.Clear();
                }
                return;
            }

            var normalized = raw.Replace(" ", string.Empty).Replace(".", string.Empty).Replace(",", string.Empty);
            decimal value;
            if (decimal.TryParse(normalized, NumberStyles.Number, CultureInfo.InvariantCulture, out value))
            {
                apply(value);
                if (ModelState.ContainsKey(key))
                {
                    ModelState[key].Errors.Clear();
                }
            }
        }

        // GET: Products/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Product product = db.Products.Find(id);
            db.Products.Remove(product);
            db.SaveChanges();
            return RedirectToAction("Index");
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
