using BookingSystem.DTO.Child.ChildMenu;
using BookingSystem.DTO.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingSystem.DTO.Master.MenuDTO
{
    public class UpdateMenuDTO
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int RoleId { get; set; }

        public bool Status { get; set; }

        public List<DropdownDTO>? roleList { get; set; }

        public List<InsertChildMenuDTO>? subMenuList { get; set; }
    }
}
