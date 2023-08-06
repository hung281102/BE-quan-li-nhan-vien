using BL.BaseBL;
using Common.Attributes;
using Common.Entities;
using DL.EmployeeDL;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LicenseContext = OfficeOpenXml.LicenseContext;

namespace BL.EmployeeBL
{
    public class EmployeeBL : BaseBL<Employee>, IEmployeeBL
    {
        private IEmployeeDL _employeeDL;
        public EmployeeBL(IEmployeeDL employeeDL) : base(employeeDL)
        {
            _employeeDL = employeeDL;
        }
        public string GetNewEmployeeCode()
        {
            return _employeeDL.GetNewEmployeeCode();
        }
        public object PagingAndSearch(int pageIndex, int pageSize, string? keyWord)
        {
            return _employeeDL.PagingAndSearch(pageIndex, pageSize, keyWord);
        }

        public object DeleteMulty (string[] arrayID)
        {
            return _employeeDL.DeleteMulty(arrayID);
        }
        public Stream ExportExcel(string? query)
        {
            List<Employee> employees = _employeeDL.ExportExcel(query).ToList<Employee>();

            // Xuất file excel

            ExcelPackage.LicenseContext = LicenseContext.Commercial;
            var stream = new MemoryStream();
            var package = new ExcelPackage(stream);

            var workSheet = package.Workbook.Worksheets.Add("Sheet1");


            // fill header
            BindingFormatForExcel(workSheet, employees);
            for (int i = 0; i < employees.Count; i++)
            {
                // loop qua cac truong cua doi tuong nhan vien
                var propNames = employees[0].GetType().GetProperties().Where(prop => Attribute.IsDefined(prop, typeof(ColumnExcel)));
                var a = 1;
                foreach (var prop in propNames)
                {
                   
                    if (prop.Name == "CreateDate" || prop.Name == "ModifiedDate" || prop.Name == "DateOfBirth")
                    {
                        workSheet.Cells[i + 2, a].Value = String.Format("{0:dd/MM/yyyy}", prop.GetValue(employees[i]));

                    }
                    else if (prop.Name == "Gender")
                    {
                        var genderNumber = prop.GetValue(employees[i]);
                        switch (genderNumber)
                        {
                            case 1:
                                workSheet.Cells[i + 2, a].Value = "Nam";
                                break;
                            case 0:
                                workSheet.Cells[i + 2, a].Value = "Nữ";
                                break;
                            case 2:
                                workSheet.Cells[i + 2, a].Value = "Khác";
                                break;
                            default:
                                workSheet.Cells[i + 2, a].Value = "Không có dữ liệu";
                                break;
                        }
                    }
                    else
                    {
                        workSheet.Cells[i + 2, a].Value = prop.GetValue(employees[i]);
                    }
                    a++;
                }
            }
            package.Save();
            stream.Position = 0;
            return package.Stream;
        }
        private void BindingFormatForExcel(ExcelWorksheet workSheet, List<Employee> payments)
        {
            // Set default width cho tất cả column
            workSheet.DefaultColWidth = 50;

            // Tự động xuống hàng khi text quá dài
            //workSheet.Cells.Style.WrapText = true;
            var propNames = payments[0].GetType().GetProperties().Where(prop => Attribute.IsDefined(prop, typeof(ColumnExcel)));
            var current = 1;

            // Tạo header cho file
            foreach (var prop in propNames)
            {
                var propName = prop.GetCustomAttributes(typeof(ColumnExcel), true);
                workSheet.Cells[1, current].Value = (propName[0] as ColumnExcel).Name;

                current++;
            }
            using (var range = workSheet.Cells["A1:N1"])
            {
                range.Style.Fill.PatternType = ExcelFillStyle.Solid;
                range.Style.Font.Bold = true;
                range.Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml("#d9f2f7"));
            }

        }
    }
}
