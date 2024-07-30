using BookingSystem.DTO.Child.ChildResource;
using BookingSystem.DTO.Master.ResourceDTO;
using BookingSystem.Provider;
using BookingSystem.Service;
using Microsoft.AspNetCore.Mvc;

namespace BookingSystem.WEB.Controllers
{
    public class ResourceController : Controller
    {
        private ResourceService _resourceService;

        public ResourceController(ResourceService resourceService) 
        {
            this._resourceService = resourceService;
        }

        public IActionResult Index(string? resourceName, bool? status, int? page = 1)
        {
            resourceName = resourceName ?? string.Empty;

            var dto = _resourceService.GetAll(resourceName, status, page.Value);
            ViewBag.Title = "Booking Code Index";
            ViewBag.ResourceName = resourceName;
            ViewBag.Status = status;
            ViewBag.Page = page;
            return View(dto);
        }

        public IActionResult Upsert(int? resourceId)
        {
            if (resourceId != null)
            {
                var entity = _resourceService.GetOne(resourceId.Value);
                var resourceChildren = _resourceService.GetChildrenResource(resourceId.Value).ToList();
                UpdateResourceDTO updateDto = new UpdateResourceDTO()
                {
                    Id = entity.Id,
                    ResourceName = entity.ResourceName,
                    Status = entity.Status.Value,
                    IconPath = entity.IconPath,
                    ResourceChildren = resourceChildren,
                };
                return View("Update", updateDto);
            }
            InsertResourceDTO dto = new InsertResourceDTO();
            dto.ResourceChildren = new List<InsertChildResourceDTO>();
            return View("Insert", dto);
        }

        [HttpPost]
        public IActionResult Insert(InsertResourceDTO dto)
        {
            if (ModelState.IsValid)
            {
                _resourceService.InsertResource(dto);
                return RedirectToAction("Index");
            }
            return View("Insert", dto);
        }

        [HttpPost]
        public IActionResult Update(UpdateResourceDTO dto)
        {
            if (ModelState.IsValid)
            {
                _resourceService.UpdateResource(dto);
                return RedirectToAction("Index");
            }
            return View("Update", dto);
        }

        [AcceptVerbs("GET", "POST")]
        public IActionResult Delete(int resourceId)
        {
            _resourceService.DeleteResource(resourceId);
            return RedirectToAction("Index");
        }

        [HttpGet]
        [Route("InsertChild")]
        public IActionResult InsertChild(int resourceId)
        {
            var entity = _resourceService.GetOne(resourceId);
            InsertChildResourceDTO dto = new InsertChildResourceDTO()
            {
                ResourceId = resourceId
            };
            ViewBag.ResourceId = resourceId;
            return View("InsertChild", dto);
        }

        [HttpPost]
        [Route("InsertChild")]
        public IActionResult InsertChildResource(InsertChildResourceDTO dto)
        {
            if (ModelState.IsValid)
            {
                _resourceService.InsertChildResource(dto);
                return RedirectToAction("Upsert", new { resourceId = dto.ResourceId });
            }
            return View("InsertChild", dto);
        }

        [HttpGet]
        [Route("UpdateChild")]
        public IActionResult UpdateChild(int resourceId, string resourceCode)
        {
            var entity = _resourceService.GetOne(resourceId);
            UpdateChildResourceDTO dto = new UpdateChildResourceDTO()
            {
                ResourceId = resourceId,
                ResourceCode = resourceCode,
                Status = entity.Status.Value
            };
            ViewBag.ResourceId = resourceId;
            return View("UpdateChild", dto);
        }

        [HttpPost]
        [Route("UpdateChild")]
        public IActionResult UpdateChildResource(UpdateChildResourceDTO dto)
        {
            if (ModelState.IsValid)
            {
                _resourceService.UpdateChildResource(dto);
                return RedirectToAction("Upsert", new { resourceId = dto.ResourceId });
            }
            ViewBag.ResourceId = dto.ResourceId;
            return View("UpdateChild", dto);
        }
    }
}
