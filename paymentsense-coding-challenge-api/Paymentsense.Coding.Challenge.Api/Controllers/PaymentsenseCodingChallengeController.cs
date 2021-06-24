using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Paymentsense.Coding.Challenge.Api.Models;
using Paymentsense.Coding.Challenge.Api.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Paymentsense.Coding.Challenge.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PaymentsenseCodingChallengeController : ControllerBase
    {
        private readonly ICountryService countryService;
        private readonly ILogger<PaymentsenseCodingChallengeController> logger;

        public PaymentsenseCodingChallengeController(ICountryService countryService, ILogger<PaymentsenseCodingChallengeController> logger)
        {
            this.countryService = countryService;
            this.logger = logger;
        }

        [HttpGet]
        public ActionResult<string> Get()
        {
            return Ok("Paymentsense Coding Challenge!");
        }

        [HttpGet]
        [Route("countries")]
        public async Task<ActionResult<Country[]>> GetCountries(string search)
        {
            try
            {
                logger.Log(LogLevel.Information, "Fetching countries");

                var countries = await countryService.GetCountries();

                return string.IsNullOrEmpty(search) ? countries : countries.Where(x => x.Name.StartsWith(search, StringComparison.InvariantCultureIgnoreCase)).ToArray();
            }
            catch (Exception ex)
            {
                logger.Log(LogLevel.Error, $"Error while fetching countries :  {ex.Message}");

                return NotFound(ex.Message);

            }
        }
    }
}
