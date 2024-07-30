using BookingSystem.DTO.Master.GlobalSetupDTO;
using BookingSystem.DTO.Master.MstRoleDTO;
using BookingSystem.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookingSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private RoleService _roleService;

        public RoleController(RoleService roleService)
        {
            this._roleService = roleService;
        }

        [HttpGet]
        public IActionResult GetRole(string? name, int? page = 1)
        {
            name = name ?? string.Empty;

            var dto = _roleService.GetAll(name, page.Value);
            return Ok(dto);
        }

        [HttpGet]
        [Route("Upsert")]
        public IActionResult GetOneRole(int roleId)
        {
            var entity = _roleService.GetOne(roleId);
            UpdateRoleDTO dto = new UpdateRoleDTO()
            {
                Id = entity.Id,
                Name = entity.Name
            };
            return Ok(dto);
        }

        [HttpPost]
        [Route("Insert")]
        public ActionResult InsertRole(InsertRoleDTO dto)
        {
            try
            {
                _roleService.InsertRole(dto);
            }
            catch (Exception e)
            {
                return Ok(e.Message);
            }
            return Ok();
        }

        [HttpPut]
        [Route("Update")]
        public ActionResult UpdateRole(UpdateRoleDTO dto)
        {
            try
            {
                _roleService.UpdateRole(dto);
            }
            catch (Exception e)
            {
                return Ok(e.Message);
            }
            return Ok();
        }

        [HttpDelete]
        [Route("Delete")]
        public IActionResult DeleteRole(int roleId)
        {
            try
            {
                _roleService.DeleteRole(roleId);
            }
            catch (Exception e)
            {

                return Ok(e.Message);
            }
            return Ok();
        }
    }
}
