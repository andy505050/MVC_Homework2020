using System;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Collections.Generic;
using System.Web.Mvc;

namespace MVC_Homework2020.Models
{
    public class 客戶資料Repository : EFRepository<客戶資料>, I客戶資料Repository
    {
        public override IQueryable<客戶資料> All()
        {
            return base.All().Where(x => x.是否已刪除 == false);
        }

        public override void Delete(客戶資料 客戶)
        {
            客戶.是否已刪除 = true;
        }

        public List<SelectListItem> 客戶分類清單(string 客戶分類 = "")
        {
            var selectList = new List<SelectListItem>();
            selectList.Add(new SelectListItem()
            {
                Text = "一般客戶",
                Value = "一般客戶",
                Selected = 客戶分類 == "一般客戶"
            });
            selectList.Add(new SelectListItem()
            {
                Text = "VIP客戶",
                Value = "VIP客戶",
                Selected = 客戶分類 == "VIP客戶"
            });
            return selectList;
        }

        public IQueryable<客戶資料> Filter(string 搜尋客戶名稱, string 客戶分類)
        {
            var data = All();
            if (!string.IsNullOrEmpty(搜尋客戶名稱))
            {
                data = data.Where(x => x.客戶名稱.Contains(搜尋客戶名稱));

            }
            if (!string.IsNullOrEmpty(客戶分類))
            {
                data = data.Where(x => x.客戶分類 == 客戶分類);
            }
            return data;
        }

        public IOrderedQueryable<客戶資料> OrderBy(IQueryable<客戶資料> data, SortInfo sortInfo)
        {
            var sortDirection = "asc";
            var currentSortCol = "ID";

            if (!string.IsNullOrEmpty(sortInfo.CurrentSortCol))
            {
                currentSortCol = sortInfo.CurrentSortCol;
            }

            if (sortInfo.CurrentSortCol == sortInfo.OriSortCol)
            {
                sortDirection = sortInfo.SortDirection;
            }

            return data.OrderBy($"{currentSortCol} {sortDirection}");
        }
    }

    public interface I客戶資料Repository : IRepository<客戶資料>
    {

    }
}