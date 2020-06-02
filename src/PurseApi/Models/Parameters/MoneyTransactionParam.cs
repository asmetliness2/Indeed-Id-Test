using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PurseApi.Models.Parameters
{

    
    public class MoneyTransactionParam
    {
        public int UserId { get; set; }
        public string Currency { get; set; }
        public decimal Sum { get; set; }
    }
}
