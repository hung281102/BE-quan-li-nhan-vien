using Common.Entities;
using DL.BaseDL;
using DL.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DL.DepartmentDL
{
    public class DepartmentDL:BaseDL<Department>, IDepartmentDL
    {
        private readonly DapperContext _dapperContext;
        public DepartmentDL(DapperContext dapperContext) : base(dapperContext)
        {
            _dapperContext = dapperContext;
        }
    }
}
