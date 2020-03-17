using System;
using ItsCoffee.Core.Entities;

namespace ItsCoffee.Core.Services
{
    public interface ILoyaltyService
    {
        void AddLoyaltyCustomer(LoyaltyCustomer customer);
        LoyaltyCustomer GetLoyaltyCustomer(Guid customerId);
        void UpdateLoyaltyCustomer(LoyaltyCustomer customer);
        void ProcessLoyaltyPoints(Order order);
    }
}