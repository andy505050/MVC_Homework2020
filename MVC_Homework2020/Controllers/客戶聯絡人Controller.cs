﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ClosedXML.Excel;
using MVC_Homework2020.Models;

namespace MVC_Homework2020.Controllers
{
    public class 客戶聯絡人Controller : Controller
    {
        private 客戶聯絡人Repository repo;
        private 客戶資料Repository repo客戶資料;
        public 客戶聯絡人Controller()
        {
            repo = RepositoryHelper.Get客戶聯絡人Repository();
            repo客戶資料 = RepositoryHelper.Get客戶資料Repository(repo.UnitOfWork);
        }

        // GET: 客戶聯絡人
        public ActionResult Index(string keyword, string 職稱, SortInfo sortInfo, string submitBtn)
        {
            var data = repo.Filter(keyword, 職稱);
            if (submitBtn == "export")
            {
                return Export(data);
            }

            data = repo.OrderBy(data, sortInfo);

            ViewBag.keyword = keyword;
            ViewBag.職稱 = repo.TitleList(職稱);
            ViewBag.SortDirection = sortInfo.SortDirection == "asc" ? "desc" : "asc";
            ViewBag.OriSortCol = sortInfo.CurrentSortCol;
            return View(data);
        }

        public ActionResult Export(IEnumerable<客戶聯絡人> 客戶聯絡人清單)
        {

            using (XLWorkbook wb = new XLWorkbook())
            {
                var data = 客戶聯絡人清單.Select(x => new { x.姓名, x.職稱, x.電話 });
                var ws = wb.Worksheets.Add("客戶聯絡人");
                ws.Cell(1, 1).Value = data;
                using (MemoryStream memoryStream = new MemoryStream())
                {
                    wb.SaveAs(memoryStream);
                    memoryStream.Seek(0, SeekOrigin.Begin);
                    return File(memoryStream.ToArray(), "application/vnd.ms-excel", "Download.xlsx");
                }
            }
        }

        // GET: 客戶聯絡人/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            客戶聯絡人 客戶聯絡人 = repo.All().FirstOrDefault(x => x.Id == id.Value);
            if (客戶聯絡人 == null)
            {
                return HttpNotFound();
            }
            return View(客戶聯絡人);
        }

        // GET: 客戶聯絡人/Create
        public ActionResult Create()
        {
            ViewBag.客戶Id = new SelectList(repo客戶資料.All(), "Id", "客戶名稱");
            return View();
        }

        // POST: 客戶聯絡人/Create
        // 若要避免過量張貼攻擊，請啟用您要繫結的特定屬性。
        // 如需詳細資料，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,客戶Id,職稱,姓名,Email,手機,電話")] 客戶聯絡人 客戶聯絡人)
        {
            if (ModelState.IsValid)
            {
                repo.Add(客戶聯絡人);
                repo.UnitOfWork.Commit();
                return RedirectToAction("Index");
            }

            ViewBag.客戶Id = new SelectList(repo客戶資料.All(), "Id", "客戶名稱", 客戶聯絡人.客戶Id);
            return View(客戶聯絡人);
        }

        // GET: 客戶聯絡人/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            客戶聯絡人 客戶聯絡人 = repo.All().FirstOrDefault(x => x.Id == id.Value);
            if (客戶聯絡人 == null)
            {
                return HttpNotFound();
            }
            ViewBag.客戶Id = new SelectList(repo客戶資料.All(), "Id", "客戶名稱", 客戶聯絡人.客戶Id);
            return View(客戶聯絡人);
        }

        // POST: 客戶聯絡人/Edit/5
        // 若要避免過量張貼攻擊，請啟用您要繫結的特定屬性。
        // 如需詳細資料，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,客戶Id,職稱,姓名,Email,手機,電話")] 客戶聯絡人 客戶聯絡人)
        {
            var ori客戶聯絡人 = repo.All().FirstOrDefault(x => x.Id == 客戶聯絡人.Id);
            if (TryUpdateModel(ori客戶聯絡人))
            {
                repo.UnitOfWork.Commit();
                return RedirectToAction("Index");
            }
            ViewBag.客戶Id = new SelectList(repo客戶資料.All(), "Id", "客戶名稱", 客戶聯絡人.客戶Id);
            return View(客戶聯絡人);
        }

        // GET: 客戶聯絡人/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            客戶聯絡人 客戶聯絡人 = repo.All().FirstOrDefault(x => x.Id == id.Value);
            if (客戶聯絡人 == null)
            {
                return HttpNotFound();
            }
            repo.Delete(客戶聯絡人);
            repo.UnitOfWork.Commit();
            return RedirectToAction("Index");
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
