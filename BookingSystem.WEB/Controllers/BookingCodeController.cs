using BookingSystem.DataModel.Master.BookingCodeVM;
using BookingSystem.DTO.Master.BookingCodeDTO;
using BookingSystem.Provider;
using Microsoft.AspNetCore.Mvc;

namespace BookingSystem.WEB.Controllers
{
    public class BookingCodeController : Controller
    {
        private BookingCodeService _bookingCodeService;

        public BookingCodeController(BookingCodeService bookingCodeService)
        {
            this._bookingCodeService = bookingCodeService;
        }

        public IActionResult Index(string? bookingCode, bool? status, int? page = 1)
        {
            bookingCode = bookingCode ?? string.Empty;

            var dto = _bookingCodeService.GetAll(bookingCode, status, page.Value);

            ViewBag.Title = "Booking Code Index";
            ViewBag.BookingCode = bookingCode; 
            ViewBag.Status = status;
            ViewBag.Page = page;

            return View(dto);
        }

        [HttpGet]
        public IActionResult Upsert(string? bookingCode)
        {
            var dtoInsert = new InsertBCDTO();
            if (bookingCode != null)
            {
                var dtoUpdate = new UpdateBCDTO();
                var entity = _bookingCodeService.GetOne(bookingCode);
                dtoUpdate.Status = entity.Status.Value;
                dtoUpdate.BookingCode = entity.BookingCode;
                return View("Update", dtoUpdate);
            }
            return View("Insert", dtoInsert);
        }

        [HttpPost]
        public IActionResult Insert(InsertBCDTO dto)
        {
            if (ModelState.IsValid)
            {
                _bookingCodeService.InsertBC(dto);
                return RedirectToAction("Index");
            }
            return View("Insert", dto);
        }

        [HttpPost]
        public IActionResult Update(UpdateBCDTO dto)
        {
            if (ModelState.IsValid)
            {
                var entity = _bookingCodeService.GetOne(dto.BookingCode);
                _bookingCodeService.UpdateBC(dto);
                return RedirectToAction("Index");
            }
            return View("Update", dto);
        }

        [AcceptVerbs("GET", "POST")]
        public IActionResult Delete(string bookingCode)
        {
            _bookingCodeService.DeleteBC(bookingCode);
            return RedirectToAction("Index");
        }
    }
}
