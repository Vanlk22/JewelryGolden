using JewelryGolden.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace JewelryGolden.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    public class UserAdminController : Controller
    {
        private JewelryDbContext db = new JewelryDbContext();

        // ===== Danh sách người dùng =====
        public ActionResult Index()
        {
            var users = db.Users.ToList();
            return View(users);
        }

        // ===== Chi tiết =====
        public ActionResult Details(string id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var user = db.Users.Find(id);
            if (user == null) return HttpNotFound();
            return View(user);
        }

        // ===== Sửa =====
        public ActionResult Edit(string id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var user = db.Users.Find(id);
            if (user == null) return HttpNotFound();
            return View(user);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,UserName,Email,PhoneNumber")] ApplicationUser model)
        {
            if (ModelState.IsValid)
            {
                var user = db.Users.Find(model.Id);
                if (user == null) return HttpNotFound();

                user.UserName = model.UserName;
                user.Email = model.Email;
                user.PhoneNumber = model.PhoneNumber;

                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(model);
        }

        // ===== Xóa =====
        public ActionResult Delete(string id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var user = db.Users.Find(id);
            if (user == null) return HttpNotFound();
            return View(user);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            var user = db.Users.Find(id);
            if (user != null)
            {
                db.Users.Remove(user);
                db.SaveChanges();
            }
            return RedirectToAction("Index");
        }
        public ActionResult Create()
        {
            return View();
        }

        // ========== TẠO MỚI (POST) ==========
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "UserName,Email,PhoneNumber,PasswordHash")] ApplicationUser user)
        {
            if (ModelState.IsValid)
            {
                var userManager = new Microsoft.AspNet.Identity.UserManager<ApplicationUser>(
                    new Microsoft.AspNet.Identity.EntityFramework.UserStore<ApplicationUser>(db)
                );

                // Nếu có nhập mật khẩu thì hash đúng chuẩn
                if (!string.IsNullOrEmpty(user.PasswordHash))
                {
                    userManager.Create(user, user.PasswordHash);
                }
                else
                {
                    // Nếu không nhập mật khẩu, tạo user mà không có mật khẩu
                    userManager.Create(user);
                }

                return RedirectToAction("Index");
            }

            return View(user);
        }



        protected override void Dispose(bool disposing)
        {
            if (disposing) db.Dispose();
            base.Dispose(disposing);
        }
    }

}
