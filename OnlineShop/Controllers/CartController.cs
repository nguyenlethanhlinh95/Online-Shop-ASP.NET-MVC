using Common;
using KetNoiCSDL.DAO;
using KetNoiCSDL.EF;
using OnlineShop.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace OnlineShop.Controllers
{
    public class CartController : Controller
    {      
        // GET: Cart
        public ActionResult Index()
        {
            var cart = Session[Common.CommonConstants.CartSession];
            var list = new List<CartItem>();
            if (cart != null)
            {
                list = (List<CartItem>)cart;
            }
            return View(list);
        }


        public ActionResult AddItem(long productId, int quantity)
        {           
            var product = new ProductDao().ViewDetail(productId);
            var cart = Session[Common.CommonConstants.CartSession];
            if (cart != null) //Neu da co hang trong gio
            {
                var list = (List<CartItem>)cart; // Ep kieu Session trong cart
                if (list.Exists(x => x.Product.ID == productId)) // tim trong danh sach co chua Id san pham chua
                {

                    foreach (var item in list)
                    {
                        if (item.Product.ID == productId)
                        {
                            item.Quantity += quantity;
                        }
                    }
                }
                else 
                {
                    //tạo mới đối tượng cart item
                    var item = new CartItem();
                    item.Product = product;
                    item.Quantity = quantity;
                    list.Add(item);
                }
                //Gán vào session
                Session[Common.CommonConstants.CartSession] = list;
            }
            else //Neu chua co hang trong gio
            {
                //tạo mới đối tượng cart item
                var item = new CartItem();
                item.Product = product;
                item.Quantity = quantity;
                var list = new List<CartItem>();
                list.Add(item);
                //Gán vào session
                Session[Common.CommonConstants.CartSession] = list;
            }
            return RedirectToAction("Index");
        }

        //Cap nhat gio hang Ajax
        public JsonResult Update(string cartModel)
        {
            var jsonCart = new JavaScriptSerializer().Deserialize<List<CartItem>>(cartModel);
            var sessionCart = (List<CartItem>)Session[Common.CommonConstants.CartSession];

            foreach (var item in sessionCart)
            {
                var jsonItem = jsonCart.SingleOrDefault(x => x.Product.ID == item.Product.ID);
                if (jsonItem != null)
                {
                    item.Quantity = jsonItem.Quantity;
                }
            }
            Session[Common.CommonConstants.CartSession] = sessionCart;
            return Json(new
            {
                status = true
            });
        }
        // End Cap nhat gio hang Ajax

        // Delete All product
        public JsonResult DeleteAll()
        {
            Session[Common.CommonConstants.CartSession] = null;
            return Json(new
            {
                status = true
            });
        }
        // End Delete All product


        // Delete product
        public JsonResult Delete(long id)
        {
            var sessionCart = (List<CartItem>)Session[Common.CommonConstants.CartSession];
            sessionCart.RemoveAll(x => x.Product.ID == id);
            Session[Common.CommonConstants.CartSession] = sessionCart;
            return Json(new
            {
                status = true
            });
        }
        // End Delete product

        // Start Payment
        [HttpGet]
        public ActionResult Payment()
        {
            var cart = Session[Common.CommonConstants.CartSession];
            var list = new List<CartItem>();
            if (cart != null)
            {
                list = (List<CartItem>)cart;
            }
            ViewBag.listProduct = list;
            return View();
        }
        // End Payment

        // Start Payment
        [HttpPost]
        public ActionResult Payment(EmailInfor emailInfor)
        {
            var order = new Order();
            order.CreatedDate = DateTime.Now;
            order.ShipAddress = emailInfor.Address;
            order.ShipMobile = emailInfor.Phone;
            order.ShipName = emailInfor.Name;
            order.ShipEmail = emailInfor.Email;

            try
            {
                var id = new OrderDao().Insert(order);
                var cart = (List<CartItem>)Session[Common.CommonConstants.CartSession];
                var detailDao = new OrderDetailDAO();
                decimal total = 0;
                foreach (var item in cart)
                {
                    var orderDetail = new OrderDetail();
                    orderDetail.ProductID = item.Product.ID;
                    orderDetail.OrderID = id;
                    orderDetail.Price = item.Product.Price;
                    orderDetail.Quantity = item.Quantity;
                    detailDao.Insert(orderDetail); // Insert Order Detail

                    total += (item.Product.Price.GetValueOrDefault(0) * item.Quantity);
                }
                string content = System.IO.File.ReadAllText(Server.MapPath("~/Assets/Client/Temp/neworder.html"));

                content = content.Replace("{{CustomerName}}", emailInfor.Name);
                content = content.Replace("{{Phone}}", emailInfor.Phone);
                content = content.Replace("{{Email}}", emailInfor.Email);
                content = content.Replace("{{Address}}", emailInfor.Address);
                content = content.Replace("{{Total}}", total.ToString("N0"));
                var toEmail = ConfigurationManager.AppSettings["ToEmailAddress"].ToString();

                new MailHelper().SendMail(emailInfor.Email, "Đơn hàng mới từ OnlineShop", content);
                new MailHelper().SendMail(toEmail, "Đơn hàng mới từ OnlineShop", content);
            }
            catch (Exception ex)
            {
                //ghi log
                return Redirect("/loi-thanh-toan");
            }
            return Redirect("/hoan-thanh");
        }
        // End Payment


        //Success
        public ActionResult Success()
        {
            return View();
        }
        //End Success
    }
}