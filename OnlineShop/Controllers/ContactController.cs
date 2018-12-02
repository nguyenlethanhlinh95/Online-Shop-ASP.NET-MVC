using KetNoiCSDL.DAO;
using KetNoiCSDL.EF;
using System.Web.Mvc;

namespace OnlineShop.Controllers
{
    public class ContactController : Controller
    {
        // GET: Contact
        public ActionResult Index()
        {
            return View();
        }

        //Form lien he
        [HttpPost]
        public ActionResult Create(Feedback fb)
        {
            if (ModelState.IsValid)
            {
                long id = new FeedBackDao().Insert(fb);
                if (id > 0)
                {
                    SetAlert("Gửi đi thành công", "success");                    
                }
                else
                {
                    SetAlert("Chưa được gửi", "error");
                }
            }
            return View("Index");
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

    }
}



