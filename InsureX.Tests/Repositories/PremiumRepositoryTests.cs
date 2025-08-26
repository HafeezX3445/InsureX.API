using InsureX.API.Models;
using InsureX.API.Repositories;
using Xunit;

namespace InsureX.Tests.Repositories
{
    public class PremiumRepositoryTests
    {
        private readonly PremiumRepository _repository;

        public PremiumRepositoryTests()
        {
            _repository = new PremiumRepository();
        }
  
        [Fact] 
        public void CalculatePremium_ShouldReturnCorrectTotal_WhenOnlySelf()
        {
            var request = new PremiumRequest
            {
                SelfAge = 25,
                Pincode = "123456",
                PhoneNumber = "9999999999"
            };

            var result = _repository.CalculatePremium(request);

            Assert.NotNull(result);
            Assert.True(result.TotalPremium > 0);
            Assert.Equal(result.TotalPremium, result.Breakdown["Self"]);
        }

        [Fact]
        public void CalculatePremium_ShouldIncludeDependents_WhenProvided()
        {
            var request = new PremiumRequest
            {
                SelfAge = 35,
                SpouseAge = 30,
                SonAge = 10,
                Pincode = "560001",
                PhoneNumber = "8888888888"
            };

            var result = _repository.CalculatePremium(request);

            Assert.True(result.Breakdown.ContainsKey("Spouse"));
            Assert.True(result.Breakdown.ContainsKey("Son"));
            Assert.Equal(result.Breakdown.Values.Sum(), result.TotalPremium);
        }

        [Theory]
        [InlineData(10, true, 5000)]   // Kid male
        [InlineData(10, false, 5000)]  // Kid female
        [InlineData(25, true, 7000)]   // Male 25
        [InlineData(25, false, 6000)]  // Female 25
        [InlineData(55, true, 25000)]  // Male 55
        [InlineData(65, false, 35000)] // Female 65
        public void CalculatePersonPremium_ShouldReturnExpectedPremium(int age, bool isMale, decimal expected)
        {
            var repo = new PremiumRepository();
            var method = repo.GetType().GetMethod("CalculatePersonPremium", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);

            var result = (decimal)method.Invoke(repo, new object[] { age, isMale });

            Assert.Equal(expected, result);
        }
    }
}
