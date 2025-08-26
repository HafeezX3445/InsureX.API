using InsureX.API.Controllers;
using InsureX.API.Models;
using InsureX.API.Repositories.IRepos;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace InsureX.Tests.Controllers
{
    public class PremiumCalculatorControllerTests
    {
        private readonly Mock<IPremiumRepository> _mockRepo;
        private readonly PremiumCalculatorController _controller;

        public PremiumCalculatorControllerTests() 
        {
            _mockRepo = new Mock<IPremiumRepository>();
            _controller = new PremiumCalculatorController(_mockRepo.Object);
        }

        [Fact]
        public void Calculate_ShouldReturnBadRequest_WhenPincodeOrPhoneMissing()
        {
            var request = new PremiumRequest
            {
                SelfAge = 25,
                PhoneNumber = ""
            };

            var result = _controller.Calculate(request);

            var badRequest = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal("Pincode and Phone Number are mandatory.", badRequest.Value);
        }

        [Fact]
        public void Calculate_ShouldReturnOk_WithValidRequest()
        {
            var request = new PremiumRequest
            {
                SelfAge = 30,
                Pincode = "560001",
                PhoneNumber = "9876543210"
            };

            var response = new PremiumResponse { TotalPremium = 10000 };
            _mockRepo.Setup(r => r.CalculatePremium(It.IsAny<PremiumRequest>())).Returns(response);

            var result = _controller.Calculate(request);

            var okResult = Assert.IsType<OkObjectResult>(result);
            var responseObj = Assert.IsType<PremiumResponse>(okResult.Value);
            Assert.Equal(10000, responseObj.TotalPremium);
        }
    }
}
