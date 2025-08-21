using InsureX.API.Models;

namespace InsureX.API.Repositories.IRepos
{
    public interface IPremiumRepository
    {
        PremiumResponse CalculatePremium(PremiumRequest request);
    }
}
