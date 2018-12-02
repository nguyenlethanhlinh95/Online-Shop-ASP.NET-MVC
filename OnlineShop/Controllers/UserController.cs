using BotDetect.Web.Mvc;
using KetNoiCSDL.DAO;
using KetNoiCSDL.EF;
using OnlineShop.Common;
using OnlineShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineShop.Controllers
{
    public class UserController : Controller
    {
        // GET: User
        public ActionResult Index()
        {
            return View();
        }

        //Dang nhap
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                var dao = new UserDao();
                var result = dao.Login(model.UserName, MaHoaMd5.MD5Hash(model.Password));
                if (result == 1)
                {
                    var user = dao.GetByID(model.UserName);
                    var userSession = new UserLogin();

                    userSession.UserName = user.UserName;
                    userSession.UserID = user.ID;
                    


                    

                    //Session user
                    Session.Add(CommonConstants.USER_SESSION, userSession);
                    return Redirect("/");
                }
                else if (result == 0)
                {
                    ModelState.AddModelError("", "Tài khoản không tồn tại.");
                }
                else if (result == -1)
                {
                    ModelState.AddModelError("", "Tài khoản đang bị khoá.");
                }
                else if (result == -2)
                {
                    ModelState.AddModelError("", "Mật khẩu không đúng.");
                }
                else
                {
                    ModelState.AddModelError("", "đăng nhập không đúng.");
                }
            }
            return View();
        }


        //Logout
        public ActionResult Logout()
        {
            Session[CommonConstants.USER_SESSION] = null;
            return Redirect("/");
        }

        //Dang ky
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [CaptchaValidation("CaptchaCode", "registerCapcha", "Mã xác nhận không đúng!")]
        public ActionResult Register(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                    var dao = new UserDao();
                    if (dao.CheckUserName(model.UserName))
                    {
                        ModelState.AddModelError("", "Tên đăng nhập đã tồn tại");
                        TempData["AlertMessage"] = "Tên đăng nhập đã tồn tại";
                        TempData["AlertType"] = "error";
                    }
                    else if (dao.CheckEmail(model.Email))
                    {
                        ModelState.AddModelError("", "Email đã tồn tại");
                        TempData["AlertMessage"] = "Email đã tồn tại";
                        TempData["AlertType"] = "error";
                    }
                    else
                    {
                        var user = new User();
                        user.UserName = model.UserName;
                        user.Name = model.Name;
                        user.Password = MaHoaMd5.MD5Hash(model.Password);
                        user.Phone = model.Phone;
                        user.Email = model.Email;
                        user.Address = model.Address;
                        user.CreatedDate = DateTime.Now;
                        user.Status = true;

                        var result = dao.Insert(user);
                        if (result > 0)
                        {
                            ViewBag.Success = "Đăng ký thành công";

                            model = new RegisterModel();
                        }
                        else
                        {
                            ModelState.AddModelError("", "Đăng ký không thành công.");
                            TempData["AlertMessage"] = "Đăng ký không thành công.";
                            TempData["AlertType"] = "error";
                        }
                    }             
            }
            return View("Register");
        }
    }
}