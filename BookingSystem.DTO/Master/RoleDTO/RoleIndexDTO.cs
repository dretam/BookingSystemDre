using BookingSystem.DTO.Master.GlobalSetupDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingSystem.DTO.Master.MstRoleDTO
{
    public class RoleIndexDTO
    {
        public IEnumerable<RoleRowDTO> Grid { get; set; }
        public int TotalPages { get; set; }
    }
}
