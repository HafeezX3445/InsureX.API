namespace InsureX.API.Models
{
    public class PremiumRequest
    {
        public string PlanType { get; set; }  // e.g. Gold, Silver
        public int SelfAge { get; set; }
        public int? SpouseAge { get; set; }
        public int? SonAge { get; set; }
        public int? DaughterAge { get; set; }

        public string Pincode { get; set; }
        public string PhoneNumber { get; set; } // mandatory
        public string Email { get; set; } // optional
        public string Name { get; set; } // optional
    }
}
