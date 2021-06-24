using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Moq;
using Paymentsense.Coding.Challenge.Api.Config;
using Paymentsense.Coding.Challenge.Api.Models;
using Paymentsense.Coding.Challenge.Api.Services;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Paymentsense.Coding.Challenge.Api.Tests.Services
{

    
    public class CountryServiceTest
    {
        private readonly HttpClient httpClient;
        private readonly Settings settings;
        CountryService sut;

        public CountryServiceTest()
        {
            var configuration = InitConfig();
            var api = configuration.GetSection("CountryApi");

            settings = new Settings
            {
                CountryApiUri = api["Uri"]
            };

            httpClient = new HttpClient();

            sut = new CountryService(httpClient, settings, new NullLogger<CountryService>());
        }


        [Fact]
        public async Task GetCoutries_Calls_Returns_List_Of_Countries()
        {
            var result = await sut.GetCountries();

            Assert.IsType<Country[]>(result);
            
        }

        private static IConfiguration InitConfig()
        {
            var config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.test.json")
                .Build();

            return config;

        }
    }
}
