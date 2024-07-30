using BookingSystem.DTO.Master.BookingCodeDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingSystem.DTO.Master.ResourceDTO
{
    public class ResourceIndexDTO
    {
        public IEnumerable<ResourceRowDTO> Grid { get; set; }
        public int TotalPages { get; set; }
    }
}
