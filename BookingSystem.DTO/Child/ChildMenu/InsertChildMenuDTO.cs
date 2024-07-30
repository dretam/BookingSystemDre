using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingSystem.DTO.Child.ChildMenu
{
    public class InsertChildMenuDTO
    {
        public int Id { get; set; }

        public int MstMenuId { get; set; }

        public string Name { get; set; }

        public bool Status { get; set; }
    }
}
