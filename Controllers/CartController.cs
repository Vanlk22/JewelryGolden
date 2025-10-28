using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using JewelryGolden.Models;
using System.Web.SessionState;

namespace JewelryGolden.Controllers
{
    public class CartController : Controller
    {
        private JewelryDbContext db = new JewelryDbContext();

        // GET: Cart - Hiển thị danh sách sản phẩm trong giỏ hàng
        public ActionResult Index()
        {
            var cart = GetCart();
            return View(cart);
        }

        // POST: Cart/AddToCart - Thêm sản phẩm vào giỏ hàng
        [HttpPost]
        public ActionResult AddToCart(int productId, int quantity = 1)
        {
            var product = db.Products.Find(productId);
            if (product == null)
            {
                return Json(new { success = false, message = "Sản phẩm không tồn tại" });
            }

            var cart = GetCart();
            var existingItem = cart.FirstOrDefault(x => x.ProductId == productId);

            if (existingItem != null)
            {
                existingItem.Quantity += quantity;
            }
            else
            {
                cart.Add(new CartItem
                {
                    ProductId = productId,
                    ProductName = product.Name,
                    ProductImage = product.Image,
                    Price = product.PromotionPrice ?? product.Price,
                    Quantity = quantity
                });
            }

            SaveCart(cart);
            return Json(new { success = true, message = "Đã thêm sản phẩm vào giỏ hàng", cartCount = cart.Sum(x => x.Quantity) });
        }

        // POST: Cart/UpdateQuantity - Cập nhật số lượng sản phẩm
        [HttpPost]
        public ActionResult UpdateQuantity(int productId, int quantity)
        {
            var cart = GetCart();
            var item = cart.FirstOrDefault(x => x.ProductId == productId);

            if (item != null)
            {
                if (quantity <= 0)
                {
                    cart.Remove(item);
                }
                else
                {
                    item.Quantity = quantity;
                }
                SaveCart(cart);
                return Json(new { success = true, cartCount = cart.Sum(x => x.Quantity) });
            }

            return Json(new { success = false, message = "Không tìm thấy sản phẩm trong giỏ hàng" });
        }

        // POST: Cart/RemoveFromCart - Xóa sản phẩm khỏi giỏ hàng
        [HttpPost]
        public ActionResult RemoveFromCart(int productId)
        {
            var cart = GetCart();
            var item = cart.FirstOrDefault(x => x.ProductId == productId);

            if (item != null)
            {
                cart.Remove(item);
                SaveCart(cart);
                return Json(new { success = true, message = "Đã xóa sản phẩm khỏi giỏ hàng", cartCount = cart.Sum(x => x.Quantity) });
            }

            return Json(new { success = false, message = "Không tìm thấy sản phẩm trong giỏ hàng" });
        }

        // GET: Cart/Checkout - Trang thanh toán
        public ActionResult Checkout()
        {
            var cart = GetCart();
            if (!cart.Any())
            {
                TempData["Message"] = "Giỏ hàng của bạn đang trống";
                return RedirectToAction("Index");
            }

            var checkoutViewModel = new CheckoutViewModel
            {
                CartItems = cart,
                TotalAmount = cart.Sum(x => x.Price * x.Quantity)
            };

            return View(checkoutViewModel);
        }

        // POST: Cart/Checkout - Xử lý đặt hàng
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Checkout(CheckoutViewModel model)
        {
            var cart = GetCart();
            if (!cart.Any())
            {
                TempData["Message"] = "Giỏ hàng của bạn đang trống";
                return RedirectToAction("Index");
            }

            if (ModelState.IsValid)
            {
                try
                {
                    // Tạo đơn hàng
                    var order = new Order
                    {
                        CustomerName = model.CustomerName,
                        CustomerAddress = model.CustomerAddress,
                        CustomerMobile = model.CustomerMobile,
                        CustomerEmail = model.CustomerEmail,
                        CustomerMessage = model.CustomerMessage,
                        PaymentMethod = model.PaymentMethod,
                        PaymentStatus = false,
                        Status = true,
                        CreatedDate = DateTime.Now
                    };

                    db.Orders.Add(order);
                    db.SaveChanges();

                    // Tạo chi tiết đơn hàng
                    foreach (var cartItem in cart)
                    {
                        var orderDetail = new OrderDetail
                        {
                            OrderID = order.ID,
                            ProductID = cartItem.ProductId,
                            Quantity = cartItem.Quantity
                        };
                        db.OrderDetails.Add(orderDetail);
                    }

                    db.SaveChanges();

                    // Xóa giỏ hàng sau khi đặt hàng thành công
                    ClearCart();

                    TempData["SuccessMessage"] = "Đặt hàng thành công! Mã đơn hàng: " + order.ID;
                    return RedirectToAction("OrderSuccess", new { orderId = order.ID });
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "Có lỗi xảy ra khi đặt hàng: " + ex.Message);
                }
            }

            model.CartItems = cart;
            model.TotalAmount = cart.Sum(x => x.Price * x.Quantity);
            return View(model);
        }

        // GET: Cart/OrderSuccess - Trang thành công đặt hàng
        public ActionResult OrderSuccess(int orderId)
        {
            var order = db.Orders.Find(orderId);
            if (order == null)
            {
                return HttpNotFound();
            }

            return View(order);
        }

        // GET: Cart/Invoice - Xem hóa đơn
        public ActionResult Invoice(int orderId)
        {
            var order = db.Orders.FirstOrDefault(o => o.ID == orderId);
            if (order == null)
            {
                return HttpNotFound();
            }

            return View(order);
        }

        // Helper methods
        private List<CartItem> GetCart()
        {
            var cart = Session["Cart"] as List<CartItem>;
            if (cart == null)
            {
                cart = new List<CartItem>();
                Session["Cart"] = cart;
            }
            return cart;
        }

        private void SaveCart(List<CartItem> cart)
        {
            Session["Cart"] = cart;
        }

        private void ClearCart()
        {
            Session["Cart"] = new List<CartItem>();
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
