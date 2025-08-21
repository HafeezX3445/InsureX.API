namespace InsureX.API.Models
{
    public class PremiumResponse
    {
        public decimal TotalPremium { get; set; }
        public Dictionary<string, decimal> Breakdown { get; set; } = new Dictionary<string, decimal>();
    }
}
