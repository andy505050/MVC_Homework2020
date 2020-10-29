using System;
using System.Linq;
using System.Collections.Generic;
using System.Data.Entity;
using System.Web.Mvc;

namespace MVC_Homework2020.Models
{
    public class 客戶聯絡人Repository : EFRepository<客戶聯絡人>, I客戶聯絡人Repository
    {
        public override IQueryable<客戶聯絡人> All()
        {
            return base.All().Include(客 => 客.客戶資料).Where(x => x.是否已刪除 == false);
        }

        public override void Delete(客戶聯絡人 entity)
        {
            entity.是否已刪除 = true;
        }

        public SelectList TitleList(string 職稱)
        {
            SelectList 職稱清單 = new SelectList(All().Select(x => new { x.職稱 }).Distinct(), "職稱", "職稱", 職稱);
            return 職稱清單;
        }

        public IQueryable<客戶聯絡人> Filter(string keyword, string 職稱)
        {
            var data = All();
            if (!string.IsNullOrEmpty(keyword))
            {
                data = data.Where(x =>
                    x.姓名.Contains(keyword) ||
                    x.Email.Contains(keyword) ||
                    x.客戶資料.客戶名稱.Contains(keyword));
            }

            if (!string.IsNullOrEmpty(職稱))
            {
                data = data.Where(x =>
                    x.職稱 == 職稱);
            }
            return data;
        }
    }

    public interface I客戶聯絡人Repository : IRepository<客戶聯絡人>
    {

    }
}