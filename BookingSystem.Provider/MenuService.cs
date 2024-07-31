using BookingSystem.DataAccess.Models;
using BookingSystem.DTO.Child.ChildMenu;
using BookingSystem.DTO.Child.ChildResource;
using BookingSystem.DTO.Master.MenuDTO;
using BookingSystem.DTO.Master.MstRoleDTO;
using BookingSystem.DTO.Master.Room;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingSystem.Service
{
    public class MenuService : BaseService
    {
        private BookingSystemContext _context;

        public MenuService(BookingSystemContext _context)
        {
            this._context = _context;
        }

        public MenuIndexDTO GetAll(string name, bool? status, int page)
        {
            var dto = new MenuIndexDTO()
            {
                Grid = new List<MenuRowDTO>()
            };

            using (var dbContext = new BookingSystemContext())
            {
                var query = from mstMenu in dbContext.MstMenus
                            where
                            (
                                mstMenu.Name.Trim().ToLower().Contains(name.Trim().ToLower())
                            )
                            && mstMenu.DeletedDate == null
                            select new MenuRowDTO()
                            {
                                Id = mstMenu.Id,
                                Name = mstMenu.Name,
                                RoleId = mstMenu.RoleId,
                                Status = (mstMenu.Status == true ? "Active" : "Deactive")
                            };

                if (status.HasValue)
                {
                    string statusString = (status.Value == true ? "Active" : "Deactive");
                    query = query.Where(x => x.Status.Trim().ToLower() == statusString.Trim().ToLower());
                }

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

        public MstMenu GetOne(int menuId)
        {
            return _context.MstMenus.SingleOrDefault(a => a.Id == menuId);
        }

        public void InsertMenu(InsertMenuDTO model)
        {
            var newMenu = new MstMenu();
            newMenu.Name = model.Name;
            newMenu.RoleId = model.RoleId;
            newMenu.Status = model.Status;
            newMenu.CreatedBy = 1;
            newMenu.CreatedDate = DateTime.UtcNow;

            _context.MstMenus.Add(newMenu);
            _context.SaveChanges();
        }

        public void UpdateMenu(UpdateMenuDTO dto)
        {
            var existedMenu = GetOne(dto.Id);
            existedMenu.Id = dto.Id;
            existedMenu.Name = dto.Name;
            existedMenu.RoleId = dto.RoleId;
            existedMenu.Status = dto.Status;
            existedMenu.UpdatedBy = 2;
            existedMenu.UpdatedDate = DateTime.UtcNow;
            _context.SaveChanges();
        }

        public void DeleteMenu(int menuId)
        {
            var existedMenu = GetOne(menuId);
            existedMenu.DeletedBy = 3;
            existedMenu.DeletedDate = DateTime.UtcNow;
            _context.SaveChanges();
        }

        public ChildMenuIndexDTO GetAllChildMenu(int menuId, string name, bool? status, int page)
        {
            var dto = new ChildMenuIndexDTO()
            {
                Grid = new List<ChildMenuRowDTO>()
            };

            using (var dbContext = new BookingSystemContext())
            {
                var query = from chlMenu in dbContext.ChlMenus
                            where
                            (
                                chlMenu.Name.Trim().ToLower().Contains(name.Trim().ToLower())
                            )
                            && chlMenu.MstMenuId == menuId
                            && chlMenu.DeletedDate == null
                            select new ChildMenuRowDTO()
                            {
                                Id = chlMenu.Id,
                                Name = chlMenu.Name,
                                MstMenuId = chlMenu.MstMenuId,
                                MstMenuName = chlMenu.MstMenu.Name,
                                Status = (chlMenu.Status == true ? "Active" : "Deactive")
                            };

                if (status.HasValue)
                {
                    string statusString = (status.Value == true ? "Active" : "Deactive");
                    query = query.Where(x => x.Status.Trim().ToLower() == statusString.Trim().ToLower());
                }

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

        public ChlMenu GetOneChildMenu(int menuId, int chlMenuId)
        {
            return _context.ChlMenus.SingleOrDefault(a => a.Id == chlMenuId && a.MstMenuId == menuId);
        }

        public List<InsertChildMenuDTO> GetChildrenMenu(int menuId)
        {
            using (var dbContext = new BookingSystemContext())
            {
                // Prepare the base query
                var query = from chlMenus in dbContext.ChlMenus
                            where chlMenus.MstMenuId == menuId
                                  && chlMenus.DeletedDate == null
                            select new InsertChildMenuDTO()
                            {
                                MstMenuId = chlMenus.MstMenuId,
                                Id = chlMenus.Id,
                                Status = chlMenus.Status.Value,
                                Name = chlMenus.Name
                            };

                // Execute the query and return the result
                return query.ToList();
            }
        }

        public void InsertChlMenu(InsertChildMenuDTO model)
        {
            var newChlMenu = new ChlMenu();
            newChlMenu.Name = model.Name;
            newChlMenu.MstMenuId = model.MstMenuId;
            newChlMenu.Status = model.Status;
            newChlMenu.CreatedBy = 1;
            newChlMenu.CreatedDate = DateTime.UtcNow;

            _context.ChlMenus.Add(newChlMenu);
            _context.SaveChanges();
        }

        public void UpdateChlMenu(UpdateChildMenuDTO dto)
        {
            var existedChlMenu = GetOneChildMenu(dto.MstMenuId, dto.Id);
            existedChlMenu.Id = dto.Id;
            existedChlMenu.Name = dto.Name;
            existedChlMenu.MstMenuId = dto.MstMenuId;
            existedChlMenu.Status = dto.Status;
            existedChlMenu.UpdatedBy = 2;
            existedChlMenu.UpdatedDate = DateTime.UtcNow;
            _context.SaveChanges();
        }

        public void DeleteChlMenu(int menuId, int chlMenuId)
        {
            var existedChlMenu = GetOneChildMenu(menuId, chlMenuId);
            existedChlMenu.DeletedBy = 3;
            existedChlMenu.DeletedDate = DateTime.UtcNow;
            _context.SaveChanges();
        }
    }
}
