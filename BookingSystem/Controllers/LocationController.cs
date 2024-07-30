using BookingSystem.DTO.Master.LocationDTO;
using BookingSystem.DTO.Master.MstRoleDTO;
using BookingSystem.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookingSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LocationController : ControllerBase
    {
        private LocationService _locationService;

        public LocationController(LocationService locationService)
        {
            this._locationService = locationService;
        }

        [HttpGet]
        public IActionResult GetLocation(string? name, int? page = 1)
        {
            name = name ?? string.Empty;

            var dto = _locationService.GetAll(name, page.Value);
            return Ok(dto);
        }

        [HttpGet]
        [Route("Upsert")]
        public IActionResult GetOneLocation(int locationId)
        {
            var entity = _locationService.GetOne(locationId);
            UpdateLocationDTO dto = new UpdateLocationDTO()
            {
                Id = entity.Id,
                Name = entity.Name
            };
            return Ok(dto);
        }

        [HttpPost]
        [Route("Insert")]
        public ActionResult InsertLocation(InsertLocationDTO dto)
        {
            try
            {
                _locationService.InsertLocation(dto);
            }
            catch (Exception e)
            {
                return Ok(e.Message);
            }
            return Ok();
        }

        [HttpPut]
        [Route("Update")]
        public ActionResult UpdateLocation(UpdateLocationDTO dto)
        {
            try
            {
                _locationService.UpdateLocation(dto);
            }
            catch (Exception e)
            {
                return Ok(e.Message);
            }
            return Ok();
        }

        [HttpDelete]
        [Route("Delete")]
        public IActionResult DeleteLocation(int locationId)
        {
            try
            {
                _locationService.DeleteLocation(locationId);
            }
            catch (Exception e)
            {

                return Ok(e.Message);
            }
            return Ok();
        }
    }
}
