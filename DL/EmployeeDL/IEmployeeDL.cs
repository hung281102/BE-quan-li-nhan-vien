using Common.Entities;
using DL.BaseDL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DL.EmployeeDL
{
    public interface IEmployeeDL : IBaseDL<Employee>
    {
        public string GetNewEmployeeCode();
        public object PagingAndSearch(int pageIndex, int pageSize, string? keyWord);
        public IEnumerable<Employee> ExportExcel(string? query);

        public object DeleteMulty(string[] arrayID);
    }
}
