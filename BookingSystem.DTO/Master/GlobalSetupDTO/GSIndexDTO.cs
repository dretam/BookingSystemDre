using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingSystem.DTO.Master.GlobalSetupDTO
{
    public class GSIndexDTO
    {
        public IEnumerable<GSRowDTO> Grid { get; set; }
        public int TotalPages { get; set; }
    }
}
