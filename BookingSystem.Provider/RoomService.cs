using BookingSystem.DataAccess.Models;
using BookingSystem.DTO.Master.BookingCodeDTO;
using BookingSystem.DTO.Master.MstRoleDTO;
using BookingSystem.DTO.Master.Room;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingSystem.Service
{
    public class RoomService : BaseService
    {
        private BookingSystemContext _context;

        public RoomService(BookingSystemContext context)
        {
            this._context = context;
        }

        public RoomIndexDTO GetAll(string name, int floor, int page)
        {
            var dto = new RoomIndexDTO()
            {
                Grid = new List<RoomRowDTO>()
            };

            using (var dbContext = new BookingSystemContext())
            {
                var query = from mstRoom in dbContext.MstRooms
                            where
                            (
                                mstRoom.Name.Trim().ToLower().Contains(name.Trim().ToLower())
                            )
                            && mstRoom.DeletedDate == null
                            select new RoomRowDTO()
                            {
                                Id = mstRoom.Id,
                                Location = mstRoom.Location.Name,
                                Name = mstRoom.Name,
                                Floor = mstRoom.Floor.Value,
                                Description = (string.IsNullOrEmpty(mstRoom.Description) ? "-" : mstRoom.Description),
                                Color = (string.IsNullOrEmpty(mstRoom.Color) ? "-" : mstRoom.Color),
                                Capacity = mstRoom.Capacity.Value
                            };

                if (floor != 0)
                {
                    query = query.Where(x => x.Floor == floor);
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

        public MstRoom GetOne(int roomId)
        {
            return _context.MstRooms.SingleOrDefault(a => a.Id == roomId);
        }

        public void InsertRoom(InsertRoomDTO model)
        {
            var newRoom = new MstRoom();
            newRoom.LocationId = model.LocationId;
            newRoom.Name = model.Name;
            newRoom.Floor = model.Floor;
            newRoom.Description = model.Description;
            newRoom.Capacity = model.Capacity;
            newRoom.Color = model.Color;
            newRoom.CreatedBy = 1;
            newRoom.CreatedDate = DateTime.UtcNow;

            _context.MstRooms.Add(newRoom);
            _context.SaveChanges();
        }

        public void UpdateRoom(UpdateRoomDTO dto)
        {
            var existedRoom = GetOne(dto.Id);
            existedRoom.Id = dto.Id;
            existedRoom.LocationId = dto.LocationId;
            existedRoom.Name = dto.Name;
            existedRoom.Floor = dto.Floor;
            existedRoom.Description = dto.Description;
            existedRoom.Capacity = dto.Capacity;
            existedRoom.Color = dto.Color;
            existedRoom.UpdatedBy = 2;
            existedRoom.UpdatedDate = DateTime.UtcNow;
            _context.SaveChanges();
        }

        public void DeleteRoom(int roomId)
        {
            var existedRoom = GetOne(roomId);
            existedRoom.DeletedBy = 3;
            existedRoom.DeletedDate = DateTime.UtcNow;
            _context.SaveChanges();
        }
    }
}
