using BookingSystem.DTO.Child.ChildMenu;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingSystem.DTO.Child.ChildResource
{
    public class ChildResourceIndexDTO
    {
        public IEnumerable<ChildResourceRowDTO> Grid { get; set; }
        public int TotalPages { get; set; }
    }
}
