using BL.EmployeeBL;
using Common.DTO;
using Common.Entities;
using Dapper;
using DL.Context;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Drawing.Printing;
using static Microsoft.Extensions.Logging.EventSource.LoggingEventSource;

namespace WebAPI_Intern.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : BaseController<Employee>
    {
        private IEmployeeBL _employeeBL;
        public EmployeeController(IEmployeeBL employeeBL) : base(employeeBL) 
        {
            _employeeBL = employeeBL;
        }

        [HttpGet("new-employee-code")]
        public IActionResult GetNewEmployeeCode()
        {
            try
            {
                var result = _employeeBL.GetNewEmployeeCode();
                if (result == null)
                {
                    return StatusCode(StatusCodes.Status404NotFound);
                }
                return StatusCode(StatusCodes.Status200OK, result);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
        [HttpGet("paging-and-search")]
        public IActionResult PagingAndSearch([FromQuery] int pageIndex, [FromQuery] int pageSize, [FromQuery] string? keyWord = "" )
        {
            try
            {
                var result = _employeeBL.PagingAndSearch(pageIndex, pageSize, keyWord);
                if (result == null)
                {
                    return StatusCode(StatusCodes.Status404NotFound);
                }
                return StatusCode(StatusCodes.Status200OK, result);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpGet("export-excel")]
        public IActionResult ExportExcel([FromQuery] string? query)
        {
            try
            {
                var result = _employeeBL.ExportExcel(query);
                if (result != null)
                {
                    return File(result, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "employee.xlsx");
                }
                else
                {
                    return StatusCode(StatusCodes.Status404NotFound);
                }
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpDelete("delete-multy")]
        public IActionResult DeleteMulty([FromBody] string[] arrayID)
        {
            try
            {
                var result = _employeeBL.DeleteMulty(arrayID);
                if (result == null)
                {
                    return StatusCode(StatusCodes.Status404NotFound);
                }
                return StatusCode(StatusCodes.Status200OK, result);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

    }
}
