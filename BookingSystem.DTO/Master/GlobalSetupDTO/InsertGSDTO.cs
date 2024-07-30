using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingSystem.DTO.Master.GlobalSetupDTO
{
    public class InsertGSDTO
    {
        public int? GSID { get; set; }

        [Required(ErrorMessage = "Parameter code is required.")]
        public string ParameterCode { get; set; }

        [Required(ErrorMessage = "Parameter name is required.")]
        public string ParameterName { get; set; }

        [Required(ErrorMessage = "Parameter desc is required.")]
        public string ParameterDesc { get; set; }

        [Required(ErrorMessage = "Parameter value is required.")]
        public string ParameterValue { get; set; }
    }
}
