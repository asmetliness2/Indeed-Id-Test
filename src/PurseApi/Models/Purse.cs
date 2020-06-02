using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PurseApi.Models
{
    public class Purse
    {
        public int Id { get; set; }
        public int UserId { get; set; }

        public IEnumerable<PurseCurrency> Currencies { get; set; }

        public Purse(int userId)
        {
            UserId = userId;
            Currencies = new List<PurseCurrency>();
        }

        public Purse(int userId, List<PurseCurrency> currencies)
        {
            UserId = userId;
            Currencies = currencies;
        }

        public PurseDto ToDto()
        {
            return new PurseDto
            {
                UserId = this.UserId,
                Currencies = this.Currencies.Select(s => new PurseCurrencyDto
                {
                    Currency = s.Currency,
                    Sum = s.Sum
                })
            };
        }
    }

    public class PurseCurrency
    {
        public int PurseId { get; set; }
        public string Currency { get; set; }
        public decimal Sum { get; set; }
    }
}
