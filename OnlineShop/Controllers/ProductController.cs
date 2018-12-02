using KetNoiCSDL.DAO;
using System;
using System.Web.Mvc;

namespace OnlineShop.Controllers
{
    public class ProductController : Controller
    {
        // GET: Product
        public ActionResult Index(int id,int page = 1, int pageSize = 1)
        {
            var dao = new ProductDao();
            var model = dao.ListAllPage(id,page, pageSize);
            return View(model);
        }

        //public ActionResult Search(int keyword, int page = 1, int pageSize = 1)
        //{
        //    var dao = new ProductDao();
        //    var model = dao.ListAllPage(id, page, pageSize);
        //    return View(model);
        //}
        [HttpGet]
        public ActionResult Search(string keyword, int page = 1, int pageSize = 1)
        {
            //Luu ket qua tim kiem
            ViewBag.keyword = keyword;
            var model = new ProductDao().Search(keyword, page, pageSize);
            return View(model);
        }

        public ActionResult Detail(long id)
        {
            var product = new ProductDao().LaySP(id);
            ViewBag.Chitiet = product;
            ViewBag.RelatedProducts = new ProductDao().ListRelatedProducts(id);
            return View();
        }

        // Get Slide bar
        [ChildActionOnly]
        public ActionResult sliderBar()
        {
            var model = new ProductCategoryDao().ListAll();
            return PartialView(model);
        }

        public JsonResult ListName(string q)
        {
            var data = new ProductDao().ListName(q);
            return Json(new
            {
                data = data,
                status = true
            },JsonRequestBehavior.AllowGet);
        }
    }
}