using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingSystem.DTO.Transaction.TrxRoomRes
{
    public class InsertTrxRoomResDTO
    {
        public int Id { get; set; }

        public int MstRoomId { get; set; }
        
        public string ChlResCode { get; set; }
    }
}
