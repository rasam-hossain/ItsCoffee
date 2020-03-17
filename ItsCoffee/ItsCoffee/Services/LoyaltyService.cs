using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using ItsCoffee.Core.Entities;
using ItsCoffee.Core.Repositories;

namespace ItsCoffee.Core.Services
{
   
    public class LoyaltyService : ILoyaltyService
    {
        private readonly ILoyaltyRepository _loyaltyRepository;

        const decimal SilverModifier = 1;
        const decimal GoldModifier = 1.25m;
        const decimal DiamondModifier = 1.5m;
        const decimal PlatinumModifier = 2m;

        const decimal SilverThreshhold = 0;
        const decimal GoldThreshhold = 20m;
        const decimal DiamondThreshhold = 100m;
        const decimal PlatinumThreshhold = 250m;

        public LoyaltyService(ILoyaltyRepository loyaltyRepository)
        {
            _loyaltyRepository = loyaltyRepository;
        }

        public void AddLoyaltyCustomer(LoyaltyCustomer customer)
        {
            _loyaltyRepository.AddLoyaltyCustomer(customer);
        }
        public LoyaltyCustomer GetLoyaltyCustomer(Guid customerId)
        {
            return _loyaltyRepository.GetLoyaltyCustomer(customerId);
        } 
        public void UpdateLoyaltyCustomer(LoyaltyCustomer customer)
        {
            _loyaltyRepository.UpdateLoyaltyCustomer(customer);
        }

        // Todo - Test 8 - Removed AddLoyaltyPointsToLoyalTyCustomer because I don't know if having it in a method makes it easier to understand

        // Todo - Test 10 - Get the reward status based on number of points
        private RewardStatus GetRewardStatusByPoints(decimal points) => points switch
        {
            var p when p >= PlatinumThreshhold => RewardStatus.Platinum,
            var p when p >= DiamondThreshhold => RewardStatus.Diamond, 
            var p when p >= GoldThreshhold => RewardStatus.Gold, 
            var p when p >= SilverThreshhold => RewardStatus.Silver, 
            _ => RewardStatus.None
        };


        public void ProcessLoyaltyPoints(Order order) 
        {
            if (order.LoyaltyCustomer == null) return;

            // Todo - Test 10 - Refactored for clarity
            order.LoyaltyCustomer.AddLoyaltyPoints(CalculateLoyaltyPoints(order));
            order.LoyaltyCustomer.RemoveLoyaltyPoints(CalculateLoyaltyPointsSpent(order));

            // Todo - Test 10 - Set the reward status
            order.LoyaltyCustomer.RewardStatus = GetRewardStatusByPoints(order.LoyaltyCustomer.LifetimeLoyaltyPoints);

            // Todo - Test 8 - Need to call the LoyaltyRepository to actually save the points
            _loyaltyRepository.UpdateLoyaltyCustomer(order.LoyaltyCustomer);
        }

        private decimal CalculateLoyaltyPoints(Order order)
        {
            decimal basePoints = order.GetEarnedLoyaltyPointsBaseAmount();

            // Todo - Test 8 - Used c# 8.0 switch expression to set the multiplier
            var multiplier = order.LoyaltyCustomer.RewardStatus switch
            {
                RewardStatus.Silver => SilverModifier,
                RewardStatus.Gold => GoldModifier,
                RewardStatus.Diamond => DiamondModifier,
                RewardStatus.Platinum => PlatinumModifier,
                _ => 1
            };
  
            return basePoints * multiplier;
        }

        // Todo - Test 9 - Created a method to calculate spent loyalty points in order to remain consistent with how they're calculated for addition
        private decimal CalculateLoyaltyPointsSpent(Order order)
        {
            return order.Payments.paymentAmounts.Where(payment => payment.Key == PaymentType.LoyaltyPoints).Sum(payment => payment.Value);
        }
    }
}