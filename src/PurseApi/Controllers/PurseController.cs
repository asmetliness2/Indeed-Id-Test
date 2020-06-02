using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PurseApi.Providers.Interfaces;

namespace PurseApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PurseController : ControllerBase
    {

        public PurseController(ICurrencyProvider currencyProvider)
        {
        }

        [HttpGet]
        public async Task<ActionResult> TestMethod()
        {
            return Ok();
        }
    }
}