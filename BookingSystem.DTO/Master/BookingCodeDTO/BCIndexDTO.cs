using BookingSystem.DTO.Master.BookingCodeDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingSystem.DataModel.Master.BookingCodeVM
{
    public class BCIndexDTO
    {
        public IEnumerable<BCRowDTO> Grid { get; set; }
        public int TotalPages { get; set; }
    }
}
