using BookingSystem.DTO.Master.MstRoleDTO;
using BookingSystem.Service;
using Microsoft.AspNetCore.Mvc;

namespace BookingSystem.WEB.Controllers
{
    [Route("Role")]
    public class RoleController : Controller
    {
        private RoleService _roleService;

        public RoleController(RoleService roleService)
        {
            this._roleService = roleService;
        }

        public IActionResult Index(string? name, int? page = 1)
        {
            name = name ?? string.Empty;
            var dto = _roleService.GetAll(name, page.Value);
            ViewBag.Title = "Role Index";
            ViewBag.Name = name;
            ViewBag.Page = page;
            return View(dto);
        }

        [HttpGet]
        [Route("Upsert")]
        public IActionResult Upsert(int? roleId)
        {
            InsertRoleDTO insertDto = new InsertRoleDTO();
            if (roleId != null) {
                var entity = _roleService.GetOne(roleId.Value);
                UpdateRoleDTO dto = new UpdateRoleDTO()
                {
                    Id = entity.Id,
                    Name = entity.Name
                };
                return View("Update", dto);
            }
            return View("Insert", insertDto);
        }

        [HttpPost]
        [Route("Insert")]
        public ActionResult Insert(InsertRoleDTO dto)
        {
            if (ModelState.IsValid)
            {            
                _roleService.InsertRole(dto);
                return RedirectToAction("Index");
            }
            return View(dto);
        }

        [HttpPost]
        [Route("Update")]
        public ActionResult Update(UpdateRoleDTO dto)
        {
            if (ModelState.IsValid)
            {            
                _roleService.UpdateRole(dto);
                return RedirectToAction("Index");
            }
            return View(dto);
        }

        [AcceptVerbs("GET", "POST")]
        [Route("Delete")]
        public IActionResult DeleteRole(int roleId)
        {
            _roleService.DeleteRole(roleId);
            return RedirectToAction("Index");
        }
    }
}
