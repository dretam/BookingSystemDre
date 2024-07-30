using BookingSystem.DTO.Master.BookingCodeDTO;
using BookingSystem.DTO.Master.Room;
using BookingSystem.Provider;
using BookingSystem.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookingSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoomController : ControllerBase
    {
        private RoomService _roomService;

        public RoomController(RoomService roomService)
        {
            this._roomService = roomService;
        }

        [HttpGet]
        public IActionResult GetRoom(string? name, int? floor = 0, int? page = 1)
        {
            name = name ?? string.Empty;
            var dto = _roomService.GetAll(name, floor.Value, page.Value);
            return Ok(dto);
        }

        [HttpGet]
        [Route("Upsert")]
        public IActionResult GetOneRoom(int roomId)
        {
            var entity = _roomService.GetOne(roomId);
            if (entity == null)
            {
                return NotFound($"Room with Id {roomId} not found.");
            }

            UpdateRoomDTO dto = new UpdateRoomDTO()
            {
                Id = entity.Id,
                LocationId = entity.LocationId,
                Name = entity.Name,
                Floor = entity.Floor.Value,
                Capacity = entity.Capacity.Value,
                Description = entity.Description,
                Color = entity.Color
            };
            return Ok(dto);
        }

        [HttpPost]
        [Route("Insert")]
        public ActionResult InsertRoom(InsertRoomDTO dto)
        {
            try
            {
                _roomService.InsertRoom(dto);
            }
            catch (Exception e)
            {
                return Ok(e.Message);
            }
            return Ok();
        }

        [HttpPost]
        [Route("Update")]
        public ActionResult UpdateRoom(UpdateRoomDTO dto)
        {
            try
            {
                var entity = _roomService.GetOne(dto.Id);
                _roomService.UpdateRoom(dto);
            }
            catch (Exception e)
            {
                return Ok(e.Message);
            }
            return Ok();
        }

        [HttpDelete]
        [Route("Delete")]
        public IActionResult DeleteRoom(int roomId)
        {
            try
            {
                _roomService.DeleteRoom(roomId);
            }
            catch (Exception e)
            {

                return Ok(e.Message);
            }
            return Ok();
        }
    }
}
