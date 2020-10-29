using System;
using System.Linq;
using System.Collections.Generic;
using System.Data.Entity;

namespace MVC_Homework2020.Models
{
	public  class 客戶聯絡人Repository : EFRepository<客戶聯絡人>, I客戶聯絡人Repository
	{
        public override IQueryable<客戶聯絡人> All()
        {
            return base.All().Include(客 => 客.客戶資料).Where(x => x.是否已刪除 == false);
        }

        public override void Delete(客戶聯絡人 entity)
        {
            entity.是否已刪除 = true;
        }
    }

	public  interface I客戶聯絡人Repository : IRepository<客戶聯絡人>
	{

	}
}