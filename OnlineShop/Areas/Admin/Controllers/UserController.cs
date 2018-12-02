using KetNoiCSDL.DAO;
using KetNoiCSDL.EF;
using OnlineShop.Common;
using System.Linq;
using System.Web.Mvc;

namespace OnlineShop.Areas.Admin.Controllers
{
    public class UserController : BaseController
    {
        // GET: Admin/User
        public ActionResult Index(string searchString, int page = 1, int pageSize = 4)
        {
            var dao = new UserDao();
            var model = dao.ListAllPaging(searchString, page, pageSize);

            //Luu ket qua tim kiem
            ViewBag.ChuoiTimKiem = searchString;
            return View(model);
        }

        // GET: Admin/User/Details/5
        public ActionResult Details(int id)
        {           
            return View();
        }

        // GET: Admin/User/Create
        [HttpGet]
        [HasCredential(RoleID ="ADD_USER")]
        public ActionResult Create()
        {

            ViewBag.list = new UserDao().GetListMember();

            
            return View();
        }



        // POST: Admin/User/Create
        [ValidateAntiForgeryToken] //bao mat
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create(User user)
        {

                // TODO: Add insert logic here
                if (ModelState.IsValid)
                {
                    var dao = new UserDao();
                    //Mã hóa mật khẩu
                    var MhMd5 = MaHoaMd5.MD5Hash(user.Password);
                    user.Password = MhMd5;
                    
                    //Kiểm tra trùng username
                    var db = new OnlineShopDbContext();
                    if (db.Users.Any(x => x.UserName == user.UserName))
                    {
                        SetAlert("Tên đăng nhập này đã tồn tại", "error");
                    //ModelState.AddModelError("Thông báo", "Tên đăng nhập này đã tồn tại");
                    }
                    else
                    {
                        
                        long id = dao.Insert(user);

                        if (id > 0)
                        {
                            SetAlert("Thêm User thành công", "success");
                            return RedirectToAction("Index", "User");
                        }                        else
                        {
                            ModelState.AddModelError("", "Thêm user không thành công.");
                        }
                    }
                                     
                }
                return View("Create");
           
        }

        // GET: Admin/User/Edit/5
        public ActionResult Edit(int id)
        {
                var user = new UserDao().ViewDetail(id);
                return View(user);
        }

        // POST: Admin/User/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(User entity)
        {
            try
            {
                // TODO: Add update logic here
                if (ModelState.IsValid)
                {
                    entity.Password = Common.MaHoaMd5.MD5Hash(entity.Password);
                    var model = new UserDao().Update(entity);
               
                }
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Admin/User/Delete/5
        [HttpDelete]
        public ActionResult Delete(int id)
        {
            var user = new UserDao().Delete(id);

            return View("Index");
        }

        // POST: Admin/User/Delete/5
        //[HttpPost]
        //public ActionResult Delete(int id, FormCollection collection)
        //{
        //    try
        //    {
        //        // TODO: Add delete logic here

        //        return RedirectToAction("Index");
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}
    }
}
