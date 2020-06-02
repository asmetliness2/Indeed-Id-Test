using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PurseApi.Models.Parameters
{

    
    public class MoneyTransactionParam
    {
        [Required(ErrorMessage = "UserId cannot be empty")]
        public int? UserId { get; set; }
        [Required(ErrorMessage = "Currency cannot be empty")]
        public string Currency { get; set; }
        [Required(ErrorMessage = "Sum cannot be empty")]
        public decimal? Sum { get; set; }
    }
}
