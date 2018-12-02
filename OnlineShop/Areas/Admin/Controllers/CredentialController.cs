using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineShop.Areas.Admin.Controllers
{
    public class CredentialController : Controller
    {
        // GET: Admin/Credential
        public ActionResult Index()
        {
            return View();
        }

        // GET: Admin/Credential/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Admin/Credential/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/Credential/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Admin/Credential/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Admin/Credential/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Admin/Credential/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Admin/Credential/Delete/5
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
