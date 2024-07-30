using BookingSystem.DTO.Child.ChildMenu;
using BookingSystem.DTO.Master.MenuDTO;
using BookingSystem.DTO.Master.Room;
using BookingSystem.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookingSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MenuController : ControllerBase
    {
        private MenuService _menuService;

        public MenuController(MenuService menuService) 
        { 
            this._menuService = menuService;
        }

        [HttpGet]
        [Route("Menu")]
        public IActionResult GetMenu(string? name, bool? status = null, int? page = 1)
        {
            name = name ?? string.Empty;
            var dto = _menuService.GetAll(name, status, page.Value);
            return Ok(dto);
        }

        [HttpGet]
        [Route("Upsert")]
        public IActionResult GetOneMenu(int menuId)
        {
            var entity = _menuService.GetOne(menuId);
            if (entity == null)
            {
                return NotFound($"Menu with Id {menuId} not found.");
            }

            UpdateMenuDTO dto = new UpdateMenuDTO()
            {
                Id = entity.Id,
                RoleId = entity.RoleId,
                Name = entity.Name,
                Status = entity.Status.Value
            };
            return Ok(dto);
        }

        [HttpPost]
        [Route("Insert")]
        public ActionResult InsertMenu(InsertMenuDTO dto)
        {
            try
            {
                _menuService.InsertMenu(dto);
            }
            catch (Exception e)
            {
                return Ok(e.Message);
            }
            return Ok();
        }

        [HttpPost]
        [Route("Update")]
        public ActionResult UpdateMenu(UpdateMenuDTO dto)
        {
            try
            {
                var entity = _menuService.GetOne(dto.Id);
                _menuService.UpdateMenu(dto);
            }
            catch (Exception e)
            {
                return Ok(e.Message);
            }
            return Ok();
        }

        [HttpDelete]
        [Route("Delete")]
        public IActionResult DeleteMenu(int menuId)
        {
            try
            {
                _menuService.DeleteMenu(menuId);
            }
            catch (Exception e)
            {

                return Ok(e.Message);
            }
            return Ok();
        }

        [HttpGet]
        [Route("ChildMenu")]
        public IActionResult GetChildMenu(int menuId, string? name, bool? status = null, int? page = 1)
        {
            name = name ?? string.Empty;
            var dto = _menuService.GetAllChildMenu(menuId, name, status, page.Value);
            return Ok(dto);
        }

        [HttpGet]
        [Route("Upsert/Child")]
        public IActionResult GetOneChildMenu(int menuId, int chlMenuId)
        {
            var entity = _menuService.GetOneChildMenu(menuId, chlMenuId);
            if (entity == null)
            {
                return NotFound($"Child menu with mstMenuId {menuId} and chlMenuId {chlMenuId} not found.");
            }

            UpdateChildMenuDTO dto = new UpdateChildMenuDTO()
            {
                Id = entity.Id,
                MstMenuId = entity.MstMenuId,
                Name = entity.Name,
                Status = entity.Status.Value
            };
            return Ok(dto);
        }

        [HttpPost]
        [Route("Insert/Child")]
        public ActionResult InsertChildMenu(InsertChildMenuDTO dto)
        {
            try
            {
                _menuService.InsertChlMenu(dto);
            }
            catch (Exception e)
            {
                return Ok(e.Message);
            }
            return Ok();
        }

        [HttpPost]
        [Route("Update/Child")]
        public ActionResult UpdateChildMenu(UpdateChildMenuDTO dto)
        {
            try
            {
                var entity = _menuService.GetOneChildMenu(dto.MstMenuId, dto.Id);
                _menuService.UpdateChlMenu(dto);
            }
            catch (Exception e)
            {
                return Ok(e.Message);
            }
            return Ok();
        }

        [HttpDelete]
        [Route("Delete/Child")]
        public IActionResult DeleteChildMenu(int menuId, int chlMenuId)
        {
            try
            {
                _menuService.DeleteChlMenu(menuId, chlMenuId);
            }
            catch (Exception e)
            {

                return Ok(e.Message);
            }
            return Ok();
        }
    }
}
