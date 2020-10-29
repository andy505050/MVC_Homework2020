using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVC_Homework2020.Models;

namespace MVC_Homework2020.Controllers
{
    public class 客戶檢視Controller : Controller
    {
        // GET: 客戶檢視
        private 客戶資料Entities db = new 客戶資料Entities();
        public ActionResult Index()
        {
            var data = db.客戶檢視;
            return View(data);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}