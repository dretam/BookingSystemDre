using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingSystem.DTO.Master.MenuDTO
{
    public class InsertMenuDTO
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int RoleId { get; set; }

        public bool Status { get; set; }
    }
}
