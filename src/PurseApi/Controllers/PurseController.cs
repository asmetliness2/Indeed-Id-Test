using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PurseApi.Models;
using PurseApi.Models.Parameters;
using PurseApi.Providers.Interfaces;
using PurseApi.Services.Interfaces;

namespace PurseApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PurseController : ControllerBase
    {
        private readonly IPurseService _purseService;
        public PurseController(IPurseService purseService)
        {
            _purseService = purseService;
        }

        [HttpGet]
        [Route("test")]
        public async Task<ActionResult> TestMethod()
        {
            return Ok("test");
        }
        /// <summary>
        /// Метод для получения состояния своего кошелька
        /// </summary>
        /// <param name="userId">Id пользователя</param>  

        [HttpGet("{userId}")]
        public async Task<ActionResult<Result<PurseDto>>> GetPurseStatus(int userId)
        {
            return await _purseService.GetPurseStatus(userId);
        }

        /// <summary>
        /// Метод для пополнения кошелька в одной из валют
        /// </summary>
        /// <returns></returns>

        [HttpPost("fill")]
        public async Task<ActionResult<Result<PurseDto>>> FillUpPurse([FromBody]MoneyTransactionParam param)
        {
            return await _purseService.FillUpPurse(param);
        }

        /// <summary>
        /// Метод для снятия денег в одной из валют
        /// </summary>
        /// <returns></returns>
        [HttpPost("withdraw")]
        public async Task<ActionResult<Result<PurseDto>>> WithdrawMoney([FromBody]MoneyTransactionParam param)
        {
            return await _purseService.WithdrawMoney(param);
        }

        /// <summary>
        /// Метод перевода денег из одной валюты в другую
        /// </summary>
        /// <returns></returns>
        [HttpPost("convert")]
        public async Task<ActionResult<Result<PurseDto>>> ConvertMoney([FromBody]ConvertCurrenciesParam param)
        {
            return await _purseService.ConvertCurrencies(param);
        }
    }
}