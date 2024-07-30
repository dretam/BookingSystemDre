using BookingSystem.DataAccess.Models;
using BookingSystem.DTO.Master.GlobalSetupDTO;
using BookingSystem.DTO.Master.MstRoleDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingSystem.Service
{
    public class RoleService : BaseService
    {
        private BookingSystemContext _context;

        public RoleService(BookingSystemContext _context)
        {
            this._context = _context;
        }

        public RoleIndexDTO GetAll(string name, int page)
        {
            var dto = new RoleIndexDTO()
            {
                Grid = new List<RoleRowDTO>()
            };

            using (var dbContext = new BookingSystemContext())
            {
                var query = from mstRole in dbContext.MstRoles
                            where
                            (
                                mstRole.Name.Trim().ToLower().Contains(name.Trim().ToLower())
                            )
                            && mstRole.DeletedDate == null
                            select new RoleRowDTO()
                            {
                                Id = mstRole.Id,
                                Name = mstRole.Name
                            };

                int totalRecords = query.Count();
                if (page > 0)
                {
                    dto.TotalPages = GetTotalPages(totalRecords);
                    query = query.Skip(GetSkip(page)).Take(_totalPage);
                }

                dto.Grid = query.ToList();
            }

            return dto;
        }

        public MstRole GetOne(int roleId)
        {
            return _context.MstRoles.SingleOrDefault(a => a.Id == roleId);
        }

        public void InsertRole(InsertRoleDTO model)
        {
            var newRole = new MstRole();
            newRole.Name = model.Name;
            newRole.CreatedBy = 1;
            newRole.CreatedDate = DateTime.UtcNow;

            _context.MstRoles.Add(newRole);
            _context.SaveChanges();
        }

        public void UpdateRole(UpdateRoleDTO model)
        {
            var existedRole = GetOne(model.Id.Value);
            existedRole.Name = model.Name;
            existedRole.UpdatedBy = 2;
            existedRole.UpdatedDate = DateTime.UtcNow;

            _context.SaveChanges();
        }

        public void DeleteRole(int roleId)
        {
            var newRole = GetOne(roleId);
            newRole.DeletedBy = 3;
            newRole.DeletedDate = DateTime.UtcNow;
            _context.SaveChanges();
        }
    }
}
