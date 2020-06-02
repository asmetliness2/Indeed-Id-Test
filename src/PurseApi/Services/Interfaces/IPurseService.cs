using PurseApi.Models;
using PurseApi.Models.Parameters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PurseApi.Services.Interfaces
{
    public interface IPurseService
    {
        Task<Result<PurseDto>> FillUpPurse(MoneyTransactionParam param);
        
        Task<Result<PurseDto>> GetPurseStatus(int userId);
       
        Task<Result<PurseDto>> WithdrawMoney(MoneyTransactionParam param);

        Task<Result<PurseDto>> ConvertCurrencies(ConvertCurrenciesParam param);

    }
}
