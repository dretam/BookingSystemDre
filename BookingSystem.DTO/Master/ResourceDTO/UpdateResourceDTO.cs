using BookingSystem.DTO.Child.ChildResource;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingSystem.DTO.Master.ResourceDTO
{
    public class UpdateResourceDTO
    {
        public int? Id { get; set; }

        [Required(ErrorMessage = "Booking code is required.")]
        public string ResourceName { get; set; }

        public bool Status { get; set; }

        public string? IconPath { get; set; }

        public List<InsertChildResourceDTO>? ResourceChildren { get; set; }
    }
}
