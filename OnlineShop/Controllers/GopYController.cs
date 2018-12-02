using Common;
using OnlineShop.Models;
using System;
using System.Configuration;
using System.Web.Mvc;

namespace OnlineShop.Controllers
{
    public class GopYController : Controller
    {
        // GET: GopY
        public ActionResult Index()
        {
            return View();
        }

        protected void SetAlert(string message, string type)
        {
            TempData["AlertMessage"] = message;
            if (type == "success")
            {
                TempData["AlertType"] = "alert-success";
            }
            else if (type == "warning")
            {
                TempData["AlertType"] = "alert-warning";
            }
            else if (type == "error")
            {
                TempData["AlertType"] = "alert-danger";
            }
        }

        [HttpPost]
        public ActionResult Send(EmailInfor EM)       
        {
            if (ModelState.IsValid)
            {
                try
                {
                    string cus = EM.Name.ToString();
                    string phone = EM.Phone.ToString();
                    string email = EM.Email.ToString();
                    string nd = EM.Content.ToString();


                    string content = System.IO.File.ReadAllText(Server.MapPath("~/Assets/Client/Temp/GopY.html"));
                    content = content.Replace("{{CustomerName}}", cus);
                    content = content.Replace("{{Phone}}", phone);
                    content = content.Replace("{{Email}}", email);
                    content = content.Replace("{{noidung}}", nd);

                    var toEmail = ConfigurationManager.AppSettings["ToEmailAddress"].ToString();
                    new MailHelper().SendMail(email, "Cảm ơn bạn đã đóng góp ý kiến", content);

                    new MailHelper().SendMail(toEmail, "Đóng góp ý kiến từ bán hàng Online", content);
                    SetAlert("Gửi đi thành công", "success");


                }
                catch (Exception ex)
                {
                    SetAlert("Chưa được gửi", "error");
                }
            }else
            {
                SetAlert("Chưa được gửi", "error");
            }
            return View("Index");
        }

    }
}