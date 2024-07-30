using BookingSystem.DTO.Master.MenuDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingSystem.DTO.Child.ChildMenu
{
    public class ChildMenuIndexDTO
    {
        public IEnumerable<ChildMenuRowDTO> Grid { get; set; }
        public int TotalPages { get; set; }
    }
}
