using BookingSystem.DTO.Master.BookingCodeDTO;
using BookingSystem.DTO.Master.GlobalSetupDTO;
using BookingSystem.Provider;
using BookingSystem.Service;
using Microsoft.AspNetCore.Mvc;

namespace BookingSystem.WEB.Controllers
{
    public class GlobalSetupController : Controller
    {
        private GlobalSetupService _globalSetupService;

        public GlobalSetupController(GlobalSetupService globalSetupService)
        {
            this._globalSetupService = globalSetupService;
        }

        public IActionResult Index(string? parameterCode, string? parameterName, int? page = 1)
        {
            parameterCode = parameterCode ?? string.Empty;
            parameterName = parameterName ?? string.Empty;
            var dto = _globalSetupService.GetAll(parameterCode, parameterName, page.Value);
            ViewBag.Title = "Global Setup Index";
            ViewBag.ParameterCode = parameterCode;
            ViewBag.ParameterName = parameterName;
            ViewBag.Page = page;
            return View(dto);
        }

        public IActionResult Upsert(int? gsId)
        {
            var dtoInsert = new InsertGSDTO();
            if (gsId != null)
            {
                var entity = _globalSetupService.GetOne(gsId.Value);
                UpdateGSDTO dto = new UpdateGSDTO()
                {
                    GSID = entity.Id,
                    ParameterCode = entity.ParameterCode,
                    ParameterName = entity.ParameterName,
                    ParameterValue = entity.ParameterValue,
                    ParameterDesc = entity.ParameterDesc
                };
                return View("Update", dto);
            }
            return View("Insert", dtoInsert);
        }

        [HttpPost]
        public IActionResult Insert(InsertGSDTO dto)
        {
            if (ModelState.IsValid)
            {
                _globalSetupService.InsertGS(dto);
                return RedirectToAction("Index");
            }
            return View("Insert", dto);
        }

        [HttpPost]
        public IActionResult Update(UpdateGSDTO dto)
        {
            if (ModelState.IsValid)
            {
                var entity = _globalSetupService.GetOne(dto.GSID);
                _globalSetupService.UpdateGS(dto);
                return RedirectToAction("Index");
            }
            return View("Update", dto);
        }

        [AcceptVerbs("GET", "POST")]
        public IActionResult Delete(int gsId)
        {
            _globalSetupService.DeleteGS(gsId);
            return RedirectToAction("Index");
        }
    }
}
