using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingSystem.DTO.Master.GlobalSetupDTO
{
    public class GSRowDTO
    {
        public int GSID { get; set; }
        public string ParameterCode { get; set; }
        public string ParameterName { get; set; }
        public string ParameterDesc { get; set; }
        public string ParameterValue { get; set; }
    }
}
