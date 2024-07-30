using BookingSystem.DTO.Master.MstRoleDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingSystem.DTO.Master.MenuDTO
{
    public class MenuIndexDTO
    {
        public IEnumerable<MenuRowDTO> Grid { get; set; }
        public int TotalPages { get; set; }
    }
}
