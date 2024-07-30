using BookingSystem.DataAccess.Models;
using BookingSystem.DTO.Master.LocationDTO;
using BookingSystem.DTO.Master.MstRoleDTO;
using BookingSystem.DTO.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace BookingSystem.Service
{
    public class LocationService : BaseService
    {
        private BookingSystemContext _context;

        public LocationService(BookingSystemContext _context)
        {
            this._context = _context;
        }

        public LocationIndexDTO GetAll(string name, int page)
        {
            var dto = new LocationIndexDTO()
            {
                Grid = new List<LocationRowDTO>()
            };

            using (var dbContext = new BookingSystemContext())
            {
                var query = from mstLocation in dbContext.MstLocations
                            where
                            (
                                mstLocation.Name.Trim().ToLower().Contains(name.Trim().ToLower())
                            )
                            && mstLocation.DeletedDate == null
                            select new LocationRowDTO()
                            {
                                Id = mstLocation.Id,
                                Name = mstLocation.Name
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

        public MstLocation GetOne(int locationId)
        {
            return _context.MstLocations.SingleOrDefault(a => a.Id == locationId);
        }

        public void InsertLocation(InsertLocationDTO model)
        {
            var newLocation = new MstLocation();
            newLocation.Name = model.Name;
            newLocation.CreatedBy = 1;
            newLocation.CreatedDate = DateTime.UtcNow;

            _context.MstLocations.Add(newLocation);
            _context.SaveChanges();
        }

        public void UpdateLocation(UpdateLocationDTO model)
        {
            var existedLocation = GetOne(model.Id);
            existedLocation.Name = model.Name;
            existedLocation.UpdatedBy = 2;
            existedLocation.UpdatedDate = DateTime.UtcNow;

            _context.SaveChanges();
        }

        public void DeleteLocation(int locationId)
        {
            var newLocation = GetOne(locationId);
            newLocation.DeletedBy = 3;
            newLocation.DeletedDate = DateTime.UtcNow;
            _context.SaveChanges();
        }

        public List<DropdownDTO> LocationDropdown()
        {
            List<DropdownDTO> dtoList;
            using (var dbContext = new BookingSystemContext())
            {
                var query = from mstLocation in dbContext.MstLocations
                            where mstLocation.DeletedDate == null
                            select new DropdownDTO()
                            {
                                Value = mstLocation.Id,
                                Text = mstLocation.Name
                            };
                dtoList = query.ToList();
            }
            return dtoList;
        }
    }
}
