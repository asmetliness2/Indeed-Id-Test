using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PurseApi.Models.Parameters
{
    public class ConvertCurrenciesParam: MoneyTransactionParam
    {
        [Required(ErrorMessage = "Currency cannot be empty")]
        public string ToCurrency { get; set; }
    }
}
