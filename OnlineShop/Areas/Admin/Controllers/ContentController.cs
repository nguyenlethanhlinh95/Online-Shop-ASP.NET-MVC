using KetNoiCSDL.DAO;
using KetNoiCSDL.EF;
using OnlineShop.Areas.Admin.Models;
using OnlineShop.Common;
using System.Web.Mvc;
using System.Web.Security.AntiXss;

namespace OnlineShop.Areas.Admin.Controllers
{
    public class ContentController : BaseController
    {
        // GET: Admin/Content
        public ActionResult Index(int page = 1, int pageSize = 5)
        {
            var model = new ContentDao().ListAllPaging(page, pageSize);
            ViewBag.Page = page;
            return View(model);
        }

        // GET: Admin/Content/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Admin/Content/Create
        public ActionResult Create()
        {
            SetViewBag();
            return View();
        }
        public void SetViewBag(long? selectedID = null)
        {
            var dao = new CategoryDao();
            ViewBag.CategoryID = new SelectList(dao.ListAll(), "ID", "Name",selectedID);
        }
        // POST: Admin/Content/Create
        [HttpPost]
        [ValidateAntiForgeryToken] //bao mat
        public ActionResult Create(ContentModel model)
        {
                // TODO: Add insert logic here
                if (ModelState.IsValid)
                {
                    var session = (UserLogin)Session[CommonConstants.USER_SESSION];
                     model.CreatedBy = session.UserName.ToString();
                    var dao = new ContentDao();
                    var content = new Content();

                    content.CategoryID = model.CategoryID;
                    content.CreatedDate = model.CreatedDate;
                    content.Description = model.Description;
                    content.Detail = AntiXssEncoder.XmlAttributeEncode(content.Detail);

                    content.Image = model.Image;
                    content.Language = model.Language;
                    content.MetaDescriptions = model.MetaDescriptions;
                    content.MetaKeywords = model.MetaKeywords;
                    content.MetaDescriptions = model.MetaDescriptions;
                    content.Name = model.Name;
                    content.Status = model.Status;
                    content.Tags = model.Tags;
                    content.TopHot = model.TopHot;
                
                    long id = dao.Create(content);

                    if (id > 0)
                    {
                        SetAlert("Thêm tin tức thành công", "success");
                        return RedirectToAction("Index", "Content");
                    }
                    else
                    {
                        SetAlert("Thêm tin tức không thành công", "error");
                        ModelState.AddModelError("", "Thêm Noi dung không thành công.");
                    }
                }
                SetViewBag();
                SetAlert("Thêm tin tức không thành công", "error");
                return View("Create");
           
        }

        // GET: Admin/Content/Edit/5
        public ActionResult Edit(int id)
        {
            var dao = new ContentDao();
            var content = dao.GetByID(id);
            SetViewBag(content.CategoryID);
            return View(content);
        }

        // POST: Admin/Content/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Edit(Content model)
        {
            try
            {
                // TODO: Add update logic herem
                if (ModelState.IsValid)
                {
                    model.Detail = AntiXssEncoder.XmlAttributeEncode(model.Detail);
                    new ContentDao().Update(model);

                    SetAlert("Cập nhật thành công", "success");
                    return RedirectToAction("Index");
                }

                SetAlert("Cập nhật không thành công", "success");
                SetViewBag(model.CategoryID);
                return RedirectToAction("Edit");
            }
            catch
            {
                return View();
            }
        }

        // GET: Admin/Content/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Admin/Content/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
