using System;
using System.Linq;
using System.Collections.Generic;

namespace MVC_Homework2020.Models
{
	public  class 客戶檢視Repository : EFRepository<客戶檢視>, I客戶檢視Repository
	{

	}

	public  interface I客戶檢視Repository : IRepository<客戶檢視>
	{

	}
}