using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVC_Homework2020.Models;

namespace MVC_Homework2020.Controllers
{
    public class 客戶資料Controller : Controller
    {
        客戶資料Entities db = new 客戶資料Entities();
        // GET: 客戶資料
        public ActionResult Index(string 搜尋客戶名稱)
        {
            var data = db.客戶資料.AsQueryable();

            if (!string.IsNullOrEmpty(搜尋客戶名稱))
            {
                data = data.Where(x => x.客戶名稱.Contains(搜尋客戶名稱));
                ViewBag.搜尋客戶名稱 = 搜尋客戶名稱;
            }

            return View(data);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(客戶資料 客戶)
        {
            if (ModelState.IsValid)
            {
                db.客戶資料.Add(客戶);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(客戶);
        }
    }
}