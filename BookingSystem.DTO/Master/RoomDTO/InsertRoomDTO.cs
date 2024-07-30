using BookingSystem.DTO.Master.LocationDTO;
using BookingSystem.DTO.Master.ResourceDTO;
using BookingSystem.DTO.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingSystem.DTO.Master.Room
{
    public class InsertRoomDTO
    {
        public int Id { get; set; }

        public int LocationId { get; set; }

        public string Name { get; set; }

        public int Floor { get; set; }

        public string Description { get; set; }

        public int Capacity { get; set; }

        public string? Color { get; set; }

        public List<DropdownDTO>? locationList { get; set; }

        //public List<DropdownDTO> resourceList { get; set; }

        //public List<ReturnCheckBoxDTO> returnCheckedResourceList { get; set; } = new List<ReturnCheckBoxDTO>();
    }
}
