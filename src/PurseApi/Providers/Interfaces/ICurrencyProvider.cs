using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PurseApi.Providers.Interfaces
{
    interface ICurrencyProvider
    {
        Task<IEnumerable<string>> GetAvailableCurrencies();

        Task<Dictionary<string, decimal>> GetCurrenciesRatio();
    }
}
