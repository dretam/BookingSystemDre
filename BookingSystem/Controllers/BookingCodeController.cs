using BookingSystem.DataModel.Master.BookingCodeVM;
using BookingSystem.DTO.Master.BookingCodeDTO;
using BookingSystem.Provider;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Xml.Linq;

namespace BookingSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingCodeController : ControllerBase
    {
        private BookingCodeService _bookingCodeService;

        public BookingCodeController(BookingCodeService bookingCodeService) 
        { 
            this._bookingCodeService = bookingCodeService;
        }

        [HttpGet]
        public IActionResult GetBC(string? bookingCode, bool? status, int? page = 1)
        {
            bookingCode = bookingCode ?? string.Empty;

            var dto = _bookingCodeService.GetAll(bookingCode, status, page.Value);
            return Ok(dto);
        }

        [HttpGet]
        [Route("Upsert")]
        public IActionResult GetOneBC(string bookingCode)
        {
            bookingCode = bookingCode ?? string.Empty;

            var entity = _bookingCodeService.GetOne(bookingCode);
            UpdateBCDTO dto = new UpdateBCDTO()
            {
                BookingCode = entity.BookingCode,
                Status = entity.Status.Value
            };
            return Ok(dto);
        }

        [HttpPost]
        [Route("Insert")]
        public ActionResult InsertBC(InsertBCDTO dto)
        {
            try
            {
                _bookingCodeService.InsertBC(dto);
            }
            catch (Exception e)
            {
                return Ok(e.Message);
            }
            return Ok();
        }

        [HttpPost]
        [Route("Update")]
        public ActionResult UpdateBC(UpdateBCDTO dto)
        {
            try
            {
                var entity = _bookingCodeService.GetOne(dto.BookingCode);
               _bookingCodeService.UpdateBC(dto);
            }
            catch (Exception e)
            {
                return Ok(e.Message);
            }
            return Ok();
        }

        [HttpDelete]
        [Route("Delete")]
        public IActionResult DeleteBC(string bookingCode)
        {
            try
            {
                _bookingCodeService.DeleteBC(bookingCode);
            }
            catch (Exception e)
            {

                return Ok(e.Message);
            }
            return Ok();
        }
    }
}
