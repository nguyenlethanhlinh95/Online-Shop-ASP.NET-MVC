using KetNoiCSDL.DAO;
using OnlineShop.Models;
using System.Collections.Generic;
using System.Web.Mvc;

namespace OnlineShop.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            ViewBag.TuiSlide = new SlideDao().ListAll();
            ViewBag.TuiNewPro = new ProductDao().NewProList(4);
            ViewBag.TuiFeaturePro = new ProductDao().FeatureProList(4);
            return View();
        }



        // Get Menu chinh
        [ChildActionOnly]
        public ActionResult MainMenu()
        {
            var model = new MenuDao().ListByGroupID(1);
            return PartialView(model);
        }

        // Get Header Cart
        [ChildActionOnly]
        public ActionResult HeaderCart()
        {
            var cart = Session[Common.CommonConstants.CartSession];
            var list = new List<CartItem>();
            if (cart != null)
            {
                list = (List<CartItem>)cart;
            }

            return PartialView(list);
        }

        // Get Menu top
        [ChildActionOnly]
        public ActionResult TopMenu()
        {
            var model = new MenuDao().ListByGroupID(2);
            return PartialView(model);
        }        //Lay footer        [ChildActionOnly]
        public ActionResult MenuFooter()
        {
            var model = new FooterDao().LayFooter();
            return PartialView(model);
        }        //Get danh muc san pham        [ChildActionOnly]        public ActionResult ProductCategory()
        {
            var model = new ProductCategoryDao().ListAll();
            return PartialView(model);
        }
    }
}