using BookingSystem.DataAccess.Models;
using BookingSystem.DTO.Master.Room;
using BookingSystem.DTO.Transaction.TrxRoomRes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingSystem.Service
{
    public class TransactionRoomResService : BaseService
    {
        private BookingSystemContext _context;

        public TransactionRoomResService(BookingSystemContext context)
        {
            this._context = context;
        }

        public MstRoom GetOne(int roomId)
        {
            return _context.MstRooms.SingleOrDefault(a => a.Id == roomId);
        }

        public void InsertTrxRoomRes(InsertTrxRoomResDTO model)
        {
            var newTrx = new TrxMstRoomChlResource();
            newTrx.MstRoomId = model.MstRoomId;
            newTrx.ChlResId = model.ChlResCode;
            newTrx.CreatedBy = 1;
            newTrx.CreatedDate = DateTime.UtcNow;

            _context.TrxMstRoomChlResources.Add(newTrx);
            _context.SaveChanges();
        }
    }
}
