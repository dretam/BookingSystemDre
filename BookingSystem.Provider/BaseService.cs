using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingSystem.Service
{
    public class BaseService
    {
        protected const int _totalPage = 10;

        public static int GetSkip(int page)
        {
            return (page - 1) * _totalPage;
        }

        protected static int GetTotalPages(int totalRows)
        {
            var decimalResult = (decimal)totalRows / (decimal)_totalPage;
            return (int)Math.Ceiling(decimalResult);
        }
    }
}
