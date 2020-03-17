using System;

namespace ItsCoffee.Core.Entities
{
    public class LoyaltyCustomer
    {
        public LoyaltyCustomer()
        {
            CustomerId = Guid.NewGuid();
        }
        public Guid CustomerId { get; set; }
        public String FirstName { get; set; }
        public String LastName { get; set; }
        public String EmailAddress { get; set; }
        public decimal LoyaltyPointsBalance { get; set; }
        public decimal LifetimeLoyaltyPoints { get; set; }
        public RewardStatus RewardStatus { get; set; }

        public override string ToString()
        {
            return $"{FirstName} {LastName}";
        }
    }
}
