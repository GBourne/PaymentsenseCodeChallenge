using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using Paymentsense.Coding.Challenge.Api.Config;
using Paymentsense.Coding.Challenge.Api.Controllers;
using Paymentsense.Coding.Challenge.Api.Models;
using Paymentsense.Coding.Challenge.Api.Services;
using Paymentsense.Coding.Challenge.Api.Services.Interfaces;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace Paymentsense.Coding.Challenge.Api.Tests.Controllers
{
    public class PaymentsenseCodingChallengeControllerTests
    {
        private  Mock<ICountryService> mockCountryService;
        private Mock<ILogger<PaymentsenseCodingChallengeController>> mockControllerLogger;
        private Mock<ILogger<CountryService>> mockCountryServiceLogger;
        private CountryService countryService;
        PaymentsenseCodingChallengeController sut;
        private Settings settings;

        public PaymentsenseCodingChallengeControllerTests()
        {
            var mockHttpClient = new Mock<IHttpClientFactory>();
            mockCountryService = new Mock<ICountryService>();
            mockControllerLogger = new Mock<ILogger<PaymentsenseCodingChallengeController>>();
            mockCountryServiceLogger = new Mock<ILogger<CountryService>>();

            settings = new Settings { CountryApiUri = "http:/123" };
            countryService = new CountryService(new HttpClient(), settings, mockCountryServiceLogger.Object);

            sut = new PaymentsenseCodingChallengeController(mockCountryService.Object, mockControllerLogger.Object);
        }

        [Fact]
        public void Get_OnInvoke_ReturnsExpectedMessage()
        {
           
            var result = sut.Get().Result as OkObjectResult;

            result.StatusCode.Should().Be(StatusCodes.Status200OK);
            result.Value.Should().Be("Paymentsense Coding Challenge!");
        }

        [Fact]
        public async Task GetCountries_WithInvalidApi_Returns_NotFound()
        {
            sut = new PaymentsenseCodingChallengeController(countryService, mockControllerLogger.Object);

            var result = await sut.GetCountries("");

            result.Result.Should().BeOfType<NotFoundObjectResult>();

        }

        [Theory]
        [InlineData("")]
        [InlineData("United")]
        public async Task Get_Returns_Correct_Rows(string country)
        {
            var countries = new Country[] { new Country {Name="United Kingdom" }, new Country { Name="France" } };
            mockCountryService.Setup(x => x.GetCountries()).ReturnsAsync(countries);

            var result = await sut.GetCountries(country);

            if (country == "")
            {
                result.Value.Length.Should().Be(2);
            }
            else
            {
                result.Value.Length.Should().Be(1);

                Assert.Collection(result.Value, x => x.Name.Equals("United Kingdom"));
            }
            
            mockCountryService.Verify(x => x.GetCountries(), Times.Once);

        }

       
    }
}
