using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingSystem.DTO.Master.MenuDTO
{
    public class MenuRowDTO
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int RoleId { get; set; }

        public string Status { get; set; }
    }
}
