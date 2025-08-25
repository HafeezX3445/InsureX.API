using InsureX.API.Models;
using InsureX.API.Repositories.IRepos;
using Microsoft.AspNetCore.Mvc;

namespace InsureX.API.Controllers
{
    [Route("api/PremiumCalculator")]
    [ApiController]
    public class PremiumCalculatorController : ControllerBase
    {
        private readonly IPremiumRepository _premiumRepository;

        public PremiumCalculatorController(IPremiumRepository premiumRepository)
        {
            _premiumRepository = premiumRepository;
        }

        [HttpPost("getqoute")]
        public IActionResult Calculate([FromBody] PremiumRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.Pincode) || string.IsNullOrWhiteSpace(request.PhoneNumber))
                return BadRequest("Pincode and Phone Number are mandatory.");

            var result = _premiumRepository.CalculatePremium(request);
            return Ok(result);
        }
    }
}
