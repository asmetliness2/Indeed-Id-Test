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


        public bool TryConvertCurrencies(string currencyFrom, string currencyTo, decimal ratioFrom, decimal ratioTo, decimal sum)
        {
            var purseFrom = this.Currencies.FirstOrDefault(c => c.Currency == currencyFrom);
            if(purseFrom != null)
            {
                if(purseFrom.Sum >= sum)
                {
                    var purseTo = this.Currencies.FirstOrDefault(c => c.Currency == currencyTo);
                    if(purseTo == null)
                    {
                        purseTo = new PurseCurrency
                        {
                            Currency = currencyTo,
                            PurseId = this.Id,
                            Sum = 0
                        };
                        this.Currencies.Append(purseTo);
                    }

                    purseFrom.Sum -= sum;
                    purseTo.Sum += ((sum / ratioFrom) * ratioTo);

                    return true;
                }
            }
            return false;
        }

        public bool TryWithdraw(string currency, decimal sum)
        {
            var purseCurrency = this.Currencies.FirstOrDefault(c => c.Currency == currency);
            if(purseCurrency != null)
            {
                if(purseCurrency.Sum >= sum)
                {
                    purseCurrency.Sum -= sum;
                    return true;
                }
            }
            return false;
        }

        public void FillUp(string currency, decimal sum)
        {
            var purseCurrency = this.Currencies.FirstOrDefault(c => c.Currency == currency);
            if(purseCurrency == null)
            {
                this.Currencies.Append(new PurseCurrency
                {
                    Currency = currency,
                    Sum = sum,
                    PurseId = this.Id
                });
            } 
            else
            {
                purseCurrency.Sum += sum;
            }
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
