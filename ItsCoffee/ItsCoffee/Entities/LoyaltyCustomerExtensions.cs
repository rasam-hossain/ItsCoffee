namespace ItsCoffee.Core.Entities
{
    // Todo - Test 10 - I created extension methods for adding and removing loyalty points
    // The reasons for this are:
    //  1. Improve readability (easy to read and reason about)
    //  2. Encapsulates additional logic (ie. adding to both balance and lifetime balance; preventing balance from becoming negative)
    //  3. Make no changes to LoyaltyCustomer so I don't increase risk of change
    public static class LoyaltyCustomerExtensions
    {
        public static void AddLoyaltyPoints(this LoyaltyCustomer customer, decimal pointsToAdd)
        {
            customer.LoyaltyPointsBalance += pointsToAdd;
            customer.LifetimeLoyaltyPoints += pointsToAdd;
        }

        public static void RemoveLoyaltyPoints(this LoyaltyCustomer customer, decimal pointsToRemove)
        {
            customer.LoyaltyPointsBalance -= pointsToRemove;
            customer.LoyaltyPointsBalance = customer.LoyaltyPointsBalance < 0 ? 0 : customer.LoyaltyPointsBalance;
        }
    }
}