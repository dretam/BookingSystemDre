using BookingSystem.DataModel.Master.BookingCodeVM;
using BookingSystem.DTO.Child.ChildResource;
using BookingSystem.DTO.Master.ResourceDTO;
using BookingSystem.Provider;
using BookingSystem.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookingSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ResourceController : ControllerBase
    {
        private ResourceService _resourceService;

        public ResourceController(ResourceService resourceService)
        {
            this._resourceService = resourceService;
        }

        [HttpGet]
        public IActionResult GetRes(string? resourceName, bool? status, int? page = 1)
        {
            resourceName = resourceName ?? string.Empty;

            var dto = _resourceService.GetAll(resourceName, status, page.Value);
            return Ok(dto);
        }

        [HttpGet]
        [Route("Upsert")]
        public IActionResult GetOneRes(int resourceId)
        {
            var entity = _resourceService.GetOne(resourceId);
            UpdateResourceDTO dto = new UpdateResourceDTO()
            {
                Id = entity.Id,
                ResourceName = entity.ResourceName,
                Status = entity.Status.Value,
                IconPath = entity.IconPath
            };
            return Ok(dto);
        }

        [HttpPost]
        public ActionResult Insert(InsertResourceDTO dto)
        {
            _resourceService.InsertResource(dto);
            return Ok();
        }

        [HttpPut]
        public ActionResult Update(UpdateResourceDTO dto)
        {
            _resourceService.UpdateResource(dto);
            return Ok();
        }

        [HttpDelete]
        [Route("Delete")]
        public IActionResult DeleteRes(int resourceId)
        {
            try
            {
                _resourceService.DeleteResource(resourceId);
            }
            catch (Exception e)
            {

                return Ok(e.Message);
            }
            return Ok();
        }

        [HttpGet]
        [Route("ChildResource")]
        public IActionResult GetChlRes(int resourceId, string? chlResourceCode, bool? status, int? page = 1)
        {
            chlResourceCode = chlResourceCode ?? string.Empty;

            var dto = _resourceService.GetAllChildResource(resourceId, chlResourceCode, status, page.Value);
            return Ok(dto);
        }

        [HttpGet]
        [Route("Upsert/Child")]
        public IActionResult GetOneChlRes(int resourceId, string childResourceCode)
        {
            var entity = _resourceService.GetOneChildResource(resourceId, childResourceCode);
            InsertChildResourceDTO dto = new InsertChildResourceDTO()
            {
                ResourceCode = entity.ResourceCode,
                ResourceId = entity.ResourceId,
                Status = entity.Status.Value
            };
            return Ok(dto);
        }

        [HttpPost]
        [Route("Insert/Child")]
        public ActionResult InsertChildResource(InsertChildResourceDTO dto)
        {
            try
            {
                _resourceService.InsertChildResource(dto);
            }
            catch (Exception e)
            {

                return Ok(e.Message);
            }
            return Ok();
        }

        [HttpPut]
        [Route("Update/Child")]
        public ActionResult UpdateChildResource(UpdateChildResourceDTO dto)
        {
            try
            {
                _resourceService.UpdateChildResource(dto);
            }
            catch (Exception e)
            {

                return Ok(e.Message);
            }
            return Ok();
        }

        [HttpDelete]
        [Route("Delete/Child")]
        public IActionResult DeleteChlRes(int resourceId, string resourceCode)
        {
            try
            {
                _resourceService.DeleteChildResource(resourceId, resourceCode);
            }
            catch (Exception e)
            {

                return Ok(e.Message);
            }
            return Ok();
        }
    }
}
