using InsureX.API.Models;
using InsureX.API.Repositories.IRepos;

namespace InsureX.API.Repositories
{
    public class PremiumRepository : IPremiumRepository
    {
        public PremiumResponse CalculatePremium(PremiumRequest request)
        {
            var response = new PremiumResponse();

            response.Breakdown["Self"] = CalculatePersonPremium(request.SelfAge, true);

            if (request.SpouseAge.HasValue)
                response.Breakdown["Spouse"] = CalculatePersonPremium(request.SpouseAge.Value, false);

            if (request.SonAge.HasValue)
                response.Breakdown["Son"] = CalculatePersonPremium(request.SonAge.Value, true);

            if (request.DaughterAge.HasValue)
                response.Breakdown["Daughter"] = CalculatePersonPremium(request.DaughterAge.Value, false);

            response.TotalPremium = response.Breakdown.Values.Sum();

            return response;
        }

        private decimal CalculatePersonPremium(int age, bool isMale)
        {
            if (age != 0)
            {
                if (age < 18) // kids
                    return 5000;

                if (isMale)
                {
                    if (age <= 30) return 7000;
                    else if (age <= 40) return 10000;
                    else if (age <= 50) return 15000;
                    else if (age <= 60) return 25000;
                    else return 40000;
                }
                else
                {
                    if (age <= 30) return 6000;
                    else if (age <= 40) return 8000;
                    else if (age <= 50) return 12000;
                    else if (age <= 60) return 20000;
                    else return 35000;
                }
            }
            return 0;

        }

    }
}
