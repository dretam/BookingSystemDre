using BookingSystem.DTO.Child.ChildResource;
using BookingSystem.DTO.Master.LocationDTO;
using BookingSystem.DTO.Master.ResourceDTO;
using BookingSystem.Service;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.Design;

namespace BookingSystem.WEB.Controllers
{
    public class LocationController : Controller
    {
        private LocationService _locationService;

        public LocationController(LocationService locationService)
        {
            this._locationService = locationService;
        }

        public IActionResult Index(string? name, int? page = 1)
        {
            name = name ?? string.Empty;

            var dto = _locationService.GetAll(name, page.Value);
            ViewBag.Title = "Location Index";
            ViewBag.LocationName = name;
            ViewBag.Page = page;
            return View(dto);
        }

        public IActionResult Upsert(int? locationId)
        {
            if (locationId != null)
            {
                var entity = _locationService.GetOne(locationId.Value);
                UpdateLocationDTO updateDto = new UpdateLocationDTO()
                {
                    Id = entity.Id,
                    Name = entity.Name
                };
                return View("Update", updateDto);
            }
            InsertLocationDTO dto = new InsertLocationDTO();
            return View("Insert", dto);
        }

        [HttpPost]
        public IActionResult Insert(InsertLocationDTO dto)
        {
            if (ModelState.IsValid)
            {
                _locationService.InsertLocation(dto);
                return RedirectToAction("Index");
            }
            return View("Insert", dto);
        }

        [HttpPost]
        public IActionResult Update(UpdateLocationDTO dto)
        {
            if (ModelState.IsValid)
            {
                _locationService.UpdateLocation(dto);
                return RedirectToAction("Index");
            }
            return View("Update", dto);
        }

        [AcceptVerbs("GET", "POST")]
        public IActionResult Delete(int locationId)
        {
            _locationService.DeleteLocation(locationId);
            return RedirectToAction("Index");
        }
    }
}
