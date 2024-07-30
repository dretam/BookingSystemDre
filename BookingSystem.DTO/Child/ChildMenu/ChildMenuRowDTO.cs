using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingSystem.DTO.Child.ChildMenu
{
    public class ChildMenuRowDTO
    {
        public int Id { get; set; }

        public int MstMenuId { get; set; }
        public string MstMenuName { get; set; }

        public string Name { get; set; }

        public string Status { get; set; }
    }
}
