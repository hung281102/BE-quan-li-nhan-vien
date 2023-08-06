using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.DTO
{
    public class EmployeeDTO
    {
        public Guid? EmployeeID { get; set; }
        public string EmployeeName { get; set; }
        public string EmployeeCode { get; set; }
        public int Gender { get; set; }
        public DateTime DateOfBirth { get; set; }
        public Guid DepartmentID { get; set; }
        public string  DepartmentName { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public string PositionJob { get; set; }
        public Decimal Salary { get; set; }
        public string? CreateBy { get; set; }
        public DateTime? CreateDate { get; set; }
        public string? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
    }
}
