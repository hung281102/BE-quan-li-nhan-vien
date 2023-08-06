using BL.BaseBL;
using Common.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.EmployeeBL
{
    public interface IEmployeeBL : IBaseBL<Employee>
    {
        public string GetNewEmployeeCode();
        public object PagingAndSearch(int pageIndex, int pageSize, string? keyWord);
        public Stream ExportExcel(string query = "");
        public object DeleteMulty(string[] arrayID);
    }
}
