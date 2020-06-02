using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PurseApi.Models
{
 
    public class PurseDto
    {
        public int UserId { get; set; }

        public IEnumerable<PurseCurrencyDto> Currencies { get; set; }
    }

    public class PurseCurrencyDto
    {
        public decimal Sum { get; set; }
        public string Currency { get; set; }
    }
}
