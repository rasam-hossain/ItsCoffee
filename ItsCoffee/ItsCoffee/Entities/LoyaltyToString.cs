namespace ItsCoffee.Core.Entities
{
    public static class LoyaltyToString
    {
        public static string ToLoyaltyString(this decimal value)
        {
            // Much like Simoleon from The Sims, our loyalty points have their own currency unit ☕
          
            // Todo: Test 1 - Copy and paste the coffee emoji here
            return $"☕{value:0.00}";
        }
    }
}
