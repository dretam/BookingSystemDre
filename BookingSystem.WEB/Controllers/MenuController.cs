using BookingSystem.DTO.Child.ChildMenu;
using BookingSystem.DTO.Child.ChildResource;
using BookingSystem.DTO.Master.GlobalSetupDTO;
using BookingSystem.DTO.Master.MenuDTO;
using BookingSystem.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using System.ComponentModel.Design;

namespace BookingSystem.WEB.Controllers
{
    [Route("Menu")]
    public class MenuController : Controller
    {
        private MenuService _menuService;

        private RoleService _roleService;

        public MenuController(MenuService menuService, RoleService roleService) 
        { 
            this._menuService = menuService;
            this._roleService = roleService;
        }

        public IActionResult Index(string? name, bool? status = null, int? page = 1)
        {
            name = name ?? string.Empty;
            var dto = _menuService.GetAll(name, status, page.Value);
            ViewBag.Title = "Menu Index";
            ViewBag.Name = name;
            ViewBag.Page = page;
            return View(dto);
        }

        [HttpGet]
        [Route("Upsert")]
        public IActionResult Upsert(int? menuId)
        {
            var dtoInsert = new InsertMenuDTO();
            dtoInsert.roleList = _roleService.GetRoleDropdown();
            if (menuId != null)
            {
                var entity = _menuService.GetOne(menuId.Value);

                var menuChildren = _menuService.GetChildrenMenu(menuId.Value).ToList();

                UpdateMenuDTO dto = new UpdateMenuDTO()
                {
                    Id = entity.Id,
                    RoleId = entity.RoleId,
                    Name = entity.Name,
                    Status = entity.Status.Value,
                    roleList = _roleService.GetRoleDropdown(),
                    subMenuList = menuChildren
                };
                return View("Update", dto);
            }
            return View("Insert", dtoInsert);
        }

        [HttpPost]
        [Route("Insert")]
        public IActionResult Insert(InsertMenuDTO dto)
        {
            if (ModelState.IsValid)
            {
                _menuService.InsertMenu(dto);
                return RedirectToAction("Index");
            }
            return View("Insert", dto);
        }

        [HttpPost]
        [Route("Update")]
        public IActionResult Update(UpdateMenuDTO dto)
        {
            if (ModelState.IsValid)
            {
                var entity = _menuService.GetOne(dto.Id);
                _menuService.UpdateMenu(dto);
                return RedirectToAction("Index");
            }
            return View("Update", dto);
        }

        [AcceptVerbs("GET", "POST")]
        [Route("Delete")]
        public IActionResult Delete(int menuId)
        {
            _menuService.DeleteMenu(menuId);
            return RedirectToAction("Index");
        }

        [HttpGet]
        [Route("InsertChild")]
        public IActionResult InsertChild(int menuId)
        {
            var entity = _menuService.GetOne(menuId);
            InsertChildMenuDTO dto = new InsertChildMenuDTO()
            {
                MstMenuId = entity.Id
            };
            ViewBag.MenuId = entity.Id;
            return View("InsertChild", dto);
        }

        [HttpPost]
        [Route("InsertChild")]
        public IActionResult InsertChildResource(InsertChildMenuDTO dto)
        {
            if (ModelState.IsValid)
            {
                _menuService.InsertChlMenu(dto);
                return RedirectToAction("Upsert", new { menuId = dto.MstMenuId });
            }
            return View("InsertChild", dto);
        }

        [HttpGet]
        [Route("UpdateChild")]
        public IActionResult UpdateChild(int menuId, int chlMenuId)
        {
            var entity = _menuService.GetOne(menuId);
            var childEntity = _menuService.GetOneChildMenu(menuId, chlMenuId);
            UpdateChildMenuDTO dto = new UpdateChildMenuDTO()
            {
                MstMenuId = childEntity.MstMenuId,
                Id = childEntity.Id,
                Status = childEntity.Status.Value,
                Name = childEntity.Name
            };
            ViewBag.MenuId = entity.Id;
            return View("UpdateChild", dto);
        }

        [HttpPost]
        [Route("UpdateChild")]
        public IActionResult UpdateChild(UpdateChildMenuDTO dto)
        {
            if (ModelState.IsValid)
            {
                _menuService.UpdateChlMenu(dto);
                return RedirectToAction("Upsert", new { menuId = dto.MstMenuId });
            }
            ViewBag.MenuId = dto.MstMenuId;
            return View("UpdateChild", dto);
        }
    }
}
