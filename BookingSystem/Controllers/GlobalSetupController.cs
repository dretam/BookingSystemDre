using BookingSystem.DTO.Master.BookingCodeDTO;
using BookingSystem.DTO.Master.GlobalSetupDTO;
using BookingSystem.Provider;
using BookingSystem.Service;
using Microsoft.AspNetCore.Mvc;

namespace BookingSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GlobalSetupController : ControllerBase
    {
        private GlobalSetupService _globalSetupService;

        public GlobalSetupController(GlobalSetupService globalSetupService)
        {
            this._globalSetupService = globalSetupService;
        }

        [HttpGet]
        public IActionResult GetGS(string? parameterCode, string? parameterName, int? page = 1)
        {
            parameterCode = parameterCode ?? string.Empty;
            parameterName = parameterName ?? string.Empty;

            var dto = _globalSetupService.GetAll(parameterCode, parameterName, page.Value);
            return Ok(dto);
        }

        [HttpGet]
        [Route("Upsert")]
        public IActionResult GetOneGS(int gsId)
        {
            var entity = _globalSetupService.GetOne(gsId);
            UpdateGSDTO dto = new UpdateGSDTO()
            {
                GSID = entity.Id,
                ParameterCode = entity.ParameterCode,
                ParameterName = entity.ParameterName,
                ParameterValue = entity.ParameterValue,
                ParameterDesc = entity.ParameterDesc
            };
            return Ok(dto);
        }

        [HttpPost]
        [Route("Insert")]
        public ActionResult InsertGS(InsertGSDTO dto)
        {
            try
            {
                _globalSetupService.InsertGS(dto);
            }
            catch (Exception e)
            {
                return Ok(e.Message);
            }
            return Ok();
        }

        [HttpPut]
        [Route("Update")]
        public ActionResult UpdateGS(UpdateGSDTO dto)
        {
            try
            {
                _globalSetupService.UpdateGS(dto);
            }
            catch (Exception e)
            {
                return Ok(e.Message);
            }
            return Ok();
        }

        [HttpDelete]
        [Route("Delete")]
        public IActionResult DeleteGS(int gsId)
        {
            try
            {
                _globalSetupService.DeleteGS(gsId);
            }
            catch (Exception e)
            {

                return Ok(e.Message);
            }
            return Ok();
        }
    }
}
