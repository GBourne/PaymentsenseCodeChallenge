using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Paymentsense.Coding.Challenge.Api.Config;
using Paymentsense.Coding.Challenge.Api.Models;
using Paymentsense.Coding.Challenge.Api.Services.Interfaces;
using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace Paymentsense.Coding.Challenge.Api.Services
{
    public class CountryService : ICountryService
    {
        private readonly HttpClient httpClient;
        private readonly Settings settings;
        private readonly ILogger<CountryService> logger;

        public CountryService(HttpClient httpClient, Settings settings, ILogger<CountryService> logger)
        {
            this.httpClient = httpClient;
            this.settings = settings;
            this.logger = logger;
        }

        public async Task<Country[]> GetCountries()
        {
            using (httpClient)
            {
                httpClient.DefaultRequestHeaders.Accept.Clear();

                var response = await httpClient.GetAsync(settings.CountryApiUri);

                if (!response.IsSuccessStatusCode)
                {
                    throw new Exception($"Http request to '{settings.CountryApiUri}' failed!");
                };

                logger.Log(LogLevel.Information, $"Http request to '{settings.CountryApiUri}' completed.");

                var result = await response.Content.ReadAsStringAsync();

                var countries = JsonConvert.DeserializeObject<Country[]>(result);

                return countries;
            }

        }
    }
}
