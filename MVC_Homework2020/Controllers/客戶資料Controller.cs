﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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
            var data = db.客戶資料.Where(x => x.是否已刪除 == false);
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

        public ActionResult Edit(int? id)
        {
            var 客戶 = db.客戶資料.FirstOrDefault(x => x.Id == id.Value);
            if (客戶 == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest,"找不到客戶資料");
            }
            return View(客戶);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(客戶資料 客戶)
        {
            var ori客戶 = db.客戶資料.Find(客戶.Id);
            if (TryUpdateModel(ori客戶))
            {
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(ori客戶);
        }

        public ActionResult Delete(int? id)
        {
            var 客戶 = db.客戶資料.FirstOrDefault(x => x.Id == id.Value);
            if (客戶 == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest, "找不到客戶資料");
            }

            客戶.是否已刪除 = true;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}