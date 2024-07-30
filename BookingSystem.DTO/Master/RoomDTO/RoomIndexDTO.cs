using BookingSystem.DTO.Master.MstRoleDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingSystem.DTO.Master.Room
{
    public class RoomIndexDTO
    {
        public IEnumerable<RoomRowDTO> Grid { get; set; }
        public int TotalPages { get; set; }
    }
}
