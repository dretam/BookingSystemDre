using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingSystem.DTO.Master.Room
{
    public class RoomRowDTO
    {
        public int Id { get; set; }

        public string Name { get; set; } = null!;

        public int Floor { get; set; }

        public string Description { get; set; }

        public int Capacity { get; set; }

        public string Color { get; set; }

        public string Location { get; set; }
    }
}
