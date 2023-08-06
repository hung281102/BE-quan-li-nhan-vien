using Common.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Entities
{
    public class Employee
    {
        [ColumnExcel("ID nhân viên")]
        [MaxLength(36, "ID nhân viên")]
        public Guid? EmployeeID { get; set; }

        [ColumnExcel("Tên nhân viên")]
        [MaxLength(100, "Tên nhân viên")]
        [NotEmpty("Tên nhân viên")]
        public string EmployeeName { get; set; }

        [ColumnExcel("Mã nhân viên")]
        [MaxLength(25, "Mã nhân viên")]
        [NotEmpty("Mã nhân viên")]
        public string EmployeeCode { get; set; }

        [ColumnExcel("Giới tính")]
        public int Gender { get; set; }

        [ColumnExcel("Ngày sinh")]
        public DateTime DateOfBirth { get; set; }

        [ColumnExcel("Mã phòng ban")]
        [NotEmpty("ID phòng ban")]
        public Guid DepartmentID { get; set; }

        [ColumnExcel("Địa chỉ")]
        [MaxLength(255, "Địa chỉ")]
        public string Address { get; set; }

        [ColumnExcel("Số điện thoại")]
        [MaxLength(25, "Số điện thoại")]
        public string PhoneNumber { get; set; }

        [ColumnExcel("Vị trí")]
        [MaxLength(100, "Vị trí công việc")]
        public string PositionJob  { get; set; }

        [ColumnExcel("Lương")]
        public Decimal Salary { get; set; }

        [ColumnExcel("Người tạo")]
        public string? CreateBy { get; set; }

        [ColumnExcel("Ngày tạo")]
        public DateTime? CreateDate { get; set; }

        [ColumnExcel("NGười chỉnh sửa")]
        public string? ModifiedBy { get; set; }

        [ColumnExcel("Ngày tạo")]
        public DateTime? ModifiedDate { get; set; }
    }
}
