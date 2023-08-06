using BL.BaseBL;
using Common.Other;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI_Intern.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BaseController<T> : ControllerBase
    {
        private IBaseBL<T> _baseBL;
        public BaseController(IBaseBL<T> baseBL)
        {
            _baseBL = baseBL;
        }

        [HttpGet("GetAll")]
        public IActionResult Get()
        {
            try
            {
                var result = _baseBL.Get();
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
        [HttpGet("{id}")]
        public IActionResult GetById(Guid id)
        {
            try
            {
                var result = _baseBL.GetById(id);
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

        [HttpDelete("{id}")]
        public IActionResult DeleteById(Guid id)
        {
            try
            {
                var result = _baseBL.DeleteById(id);
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

        [HttpPost]
        public IActionResult Post(T record)
        {
            try
            {
                var result = _baseBL.Post(record);
                if (result == null)
                {
                    return StatusCode(StatusCodes.Status400BadRequest);
                }
                return StatusCode(StatusCodes.Status201Created, result);
            }
            catch (ValidateException ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, ex);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPut("{id}")]
        public IActionResult Put([FromBody] T record, Guid id)
        {
            try
            {
                var result = _baseBL.Put(record, id);
                if (result == null)
                {
                    return StatusCode(StatusCodes.Status404NotFound);
                }
                return StatusCode(StatusCodes.Status200OK, result);
            }
            catch (ValidateException ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, ex);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        //[HttpGet("paging-and-search")]
        //public IActionResult PagingAndSearch([FromQuery] int pageIndex, [FromQuery] int pageSize, [FromQuery] string? keyWord = null)
        //{
        //    try
        //    {
        //        var result = _baseBL.PagingAndSearch(pageIndex, pageSize, keyWord);
        //        if (result == null)
        //        {
        //            return StatusCode(StatusCodes.Status404NotFound);
        //        }
        //        return StatusCode(StatusCodes.Status200OK, result);
        //    }
        //    catch
        //    {
        //        return StatusCode(StatusCodes.Status500InternalServerError);
        //    }
        //}
    }

}
