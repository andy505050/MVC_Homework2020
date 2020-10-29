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
        private 客戶檢視Repository repo;
        public 客戶檢視Controller()
        {
            repo = RepositoryHelper.Get客戶檢視Repository();
        }

        // GET: 客戶檢視
        public ActionResult Index()
        {
            var data = repo.All();
            return View(data);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                repo.UnitOfWork.Context.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}