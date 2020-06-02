using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PurseApi.Models.Parameters
{
    public class ConvertCurrenciesParam: MoneyTransactionParam
    {
        public string ToCurrency { get; set; }
    }
}
