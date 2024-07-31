using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingSystem.DTO.Utility
{
    public class ReturnChlResCheckBoxDTO
    {
        public required string Value { get; set; }
        public required string Text { get; set; }
        public bool IsChecked { get; set; }
        public int ResourceId { get; set; }
    }
}
