using BookingSystem.DataAccess.Models;
using BookingSystem.DataModel.Master.BookingCodeVM;
using BookingSystem.DTO.Master.BookingCodeDTO;
using BookingSystem.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace BookingSystem.Provider
{
    public class BookingCodeService : BaseService
    {
        private BookingSystemContext _context; 

        public BookingCodeService(BookingSystemContext _context) 
        {
            this._context = _context;
        }

        public BCIndexDTO GetAll(string bookingCode, bool? status, int page)
        {
            var dto = new BCIndexDTO()
            {
                Grid = new List<BCRowDTO>()
            };

            using (var dbContext = new BookingSystemContext())
            {
                var query = from mstBookingCodes in dbContext.MstBookingCodes
                            where mstBookingCodes.BookingCode.Trim().ToLower().Contains(bookingCode.Trim().ToLower()) && mstBookingCodes.DeletedDate == null
                            select new BCRowDTO()
                            {
                                BookingCode = mstBookingCodes.BookingCode,
                                Status = (mstBookingCodes.Status.Value == true ? "Available" : "Used")
                            };

                if (status.HasValue)
                {
                    var stringStatus = (status == true ? "Available" : "Used");
                    query = query.Where(x => x.Status.Trim().ToLower().Contains(stringStatus.Trim().ToLower()));
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

        public MstBookingCode GetOne(string bookingCode)
        {
            return _context.MstBookingCodes.SingleOrDefault(a => a.BookingCode == bookingCode);
        }

        public void InsertBC(InsertBCDTO model)
        {
            var newBC = new MstBookingCode();
            newBC.BookingCode = model.BookingCode;
            newBC.Status = model.Status;
            newBC.CreatedBy = 1;
            newBC.CreatedDate = DateTime.UtcNow;

            _context.MstBookingCodes.Add(newBC);
            _context.SaveChanges();
        }

        public void UpdateBC(UpdateBCDTO dto)
        {
            var newBC = GetOne(dto.BookingCode);
            newBC.Status = dto.Status;
            newBC.UpdatedBy = 2;
            newBC.UpdatedDate = DateTime.UtcNow;
            _context.SaveChanges();
        }

        public void DeleteBC(string bookingCode)
        {
            var newBC = GetOne(bookingCode);
            newBC.DeletedBy = 3;
            newBC.DeletedDate = DateTime.UtcNow;
            _context.SaveChanges();
        }
    }
}
