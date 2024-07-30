using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingSystem.DTO.Master.BookingCodeDTO
{
    public class InsertBCDTO
    {
        [Required(ErrorMessage = "Booking code is required.")]
        public string BookingCode { get; set; }

        public bool Status { get; set; }
    }
}
