using BookingSystem.DTO.Master.MstRoleDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingSystem.DTO.Master.LocationDTO
{
    public class LocationIndexDTO
    {
        public IEnumerable<LocationRowDTO> Grid { get; set; }
        public int TotalPages { get; set; }
    }
}
