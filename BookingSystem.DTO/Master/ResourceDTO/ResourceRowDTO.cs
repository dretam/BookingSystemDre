using BookingSystem.DTO.Child.ChildResource;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingSystem.DTO.Master.ResourceDTO
{
    public class ResourceRowDTO
    {
        public int Id { get; set; }

        public string ResourceName { get; set; }

        public bool Status { get; set; }

        public string StringStatus { get; set; }

        public string IconPath { get; set; }

        public int TotalResourceChildren { get; set; }
    }
}
