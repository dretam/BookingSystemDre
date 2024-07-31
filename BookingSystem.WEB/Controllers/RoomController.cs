using BookingSystem.DataAccess.Models;
using BookingSystem.DTO.Child.ChildResource;
using BookingSystem.DTO.Master.Room;
using BookingSystem.DTO.Transaction.TrxRoomRes;
using BookingSystem.Service;
using Microsoft.AspNetCore.Mvc;

namespace BookingSystem.WEB.Controllers
{
    public class RoomController : Controller
    {
        private RoomService _roomService;

        private LocationService _locationService;

        private ResourceService _resourceService;

        private TransactionRoomResService _trxRoomResService;

        public RoomController(RoomService roomService, LocationService locationService, ResourceService resourceService, TransactionRoomResService transactionRoomResService)
        {
            this._roomService = roomService;
            this._locationService = locationService;
            this._resourceService = resourceService;
            this._trxRoomResService = transactionRoomResService;
        }

        public IActionResult Index(string? name, int? floor = 0, int? page = 1)
        {
            name = name ?? string.Empty;
            var dto = _roomService.GetAll(name, floor.Value, page.Value);
            ViewBag.Title = "Room Index";
            ViewBag.Name = name;
            ViewBag.Floor = floor;
            ViewBag.Page = page;
            return View(dto);
        }

        [HttpGet]
        [Route("Upsert")]
        public IActionResult Upsert(int? roomId)
        {
            var resourceList = _resourceService.ReturnChlResourceList();
            var locationDropdown = _locationService.LocationDropdown();
            InsertRoomDTO dto = new InsertRoomDTO()
            {
                locationList = locationDropdown
            };
            if (roomId != null)
            {
                var entity = _roomService.GetOne(roomId.Value);
                if (entity == null)
                {
                    return NotFound($"Room with Id {roomId} not found.");
                }
                UpdateRoomDTO existDto = new UpdateRoomDTO()
                {
                    Id = entity.Id,
                    LocationId = entity.LocationId,
                    Name = entity.Name,
                    Floor = entity.Floor.Value,
                    Capacity = entity.Capacity.Value,
                    Description = entity.Description,
                    Color = entity.Color,
                    locationList = locationDropdown,
                    returnCheckedResourceList = resourceList
                };
                return View("Update", existDto);
            }
            return View("Insert", dto);  
        }

        [HttpPost]
        [Route("Insert")]
        public ActionResult Insert(InsertRoomDTO dto)
        {
            if (ModelState.IsValid)
            {
                _roomService.InsertRoom(dto);
                return RedirectToAction("Index");
            }
            return View("Insert", dto);
        }

        [HttpPost]
        [Route("Update")]
        public ActionResult Update(UpdateRoomDTO dto)
        {
            if (ModelState.IsValid)
            {
                _roomService.UpdateRoom(dto);
                var trxDto = new InsertTrxRoomResDTO();
                if (dto.returnCheckedResourceList.Count > 0)
                {
                    foreach (var item in dto.returnCheckedResourceList)
                    {
                        if (item.IsChecked == true)
                        {
                            trxDto.ChlResCode = item.Value;
                            trxDto.MstRoomId = dto.Id;
                            var chlResDto = new UpdateChildResourceDTO()
                            {
                                ResourceCode = item.Value,
                                ResourceId = item.ResourceId,
                                Status = false
                            };
                            _resourceService.UpdateChildResource(chlResDto);
                            _trxRoomResService.InsertTrxRoomRes(trxDto);
                        }
                    }
                }
                return RedirectToAction("Index");
            }
            return View("Update", dto);
        }

        [HttpDelete]
        [Route("Delete")]
        public IActionResult Delete(int roomId)
        {
            _roomService.DeleteRoom(roomId);
            return RedirectToAction("Index");
        }
    }
}
