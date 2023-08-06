using BL.BaseBL;
using Common.Entities;
using DL.DepartmentDL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.DepartmentBL
{
    public class DepartmentBL : BaseBL<Department>, IDepartmentBL
    {
        private IDepartmentDL _departmentDL;
        public DepartmentBL(IDepartmentDL departmentDL) : base(departmentDL)
        {
            _departmentDL = departmentDL;
        }
    }
}
