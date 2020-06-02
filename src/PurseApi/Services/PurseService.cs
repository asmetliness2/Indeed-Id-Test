using PurseApi.Models;
using PurseApi.Models.Parameters;
using PurseApi.Providers.Interfaces;
using PurseApi.Repository.Interfaces;
using PurseApi.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PurseApi.Services
{
    public class PurseService : IPurseService
    {
        private readonly IPurseRepository _purseRepository;
        private readonly ICurrencyProvider _currencyProvider;

        public PurseService(IPurseRepository purseRepository, ICurrencyProvider currencyProvider)
        {
            _currencyProvider = currencyProvider;
            _purseRepository = purseRepository;
        }

        public async Task<Result<PurseDto>> GetPurseStatus(int userId)
        {
            var purse = await _purseRepository.GetOrCreatePurse(userId);

            return new Result<PurseDto>(purse.ToDto());
        }

        public async Task<Result<PurseDto>> FillUpPurse(MoneyTransactionParam param)
        {
            var availableCurrencies = await _currencyProvider.GetAvailableCurrencies();
            if(!availableCurrencies.Contains(param.Currency))
            {
                return new Result<PurseDto>().SetUnprocessable($"Currency {param.Currency} not supported");
            }

            var purse = await _purseRepository.GetOrCreatePurse(param.UserId.Value);

            purse.FillUp(param.Currency, param.Sum.Value);

            await _purseRepository.Commit();

            return new Result<PurseDto>(purse.ToDto());
        }

        public async Task<Result<PurseDto>> WithdrawMoney(MoneyTransactionParam param)
        {
            var availableCurrencies = await _currencyProvider.GetAvailableCurrencies();
            if (!availableCurrencies.Contains(param.Currency))
            {
                return new Result<PurseDto>().SetUnprocessable($"Currency {param.Currency} not supported");
            }

            var purse = await _purseRepository.GetOrCreatePurse(param.UserId.Value);

            if(!purse.TryWithdraw(param.Currency, param.Sum.Value))
            {
                return new Result<PurseDto>().SetUnprocessable("Not Enough Money");
            }

            await _purseRepository.Commit();

            return new Result<PurseDto>(purse.ToDto());
        }

        public async Task<Result<PurseDto>> ConvertCurrencies(ConvertCurrenciesParam param)
        {
            var availableCurrencies = await _currencyProvider.GetAvailableCurrencies();
            if(!availableCurrencies.Contains(param.Currency) || !availableCurrencies.Contains(param.ToCurrency))
            {
                return new Result<PurseDto>().SetUnprocessable("Currency not supported");
            }

            var ratio = await _currencyProvider.GetCurrenciesRatio();

            var purse = await _purseRepository.GetOrCreatePurse(param.UserId.Value);

            if(!purse.TryConvertCurrencies(param.Currency, param.ToCurrency, ratio[param.Currency], ratio[param.ToCurrency], param.Sum.Value))
            {
                return new Result<PurseDto>().SetUnprocessable("Not Enough Money");
            }

            await _purseRepository.Commit();

            return new Result<PurseDto>(purse.ToDto());
        }

    }
}
