using System;
using System.Collections.Generic;
using ItsCoffee.Core.Entities;

namespace ItsCoffee.Core.Repositories
{
    public interface ILoyaltyRepository
    {
        void AddLoyaltyCustomer(LoyaltyCustomer customer);

        void UpdateLoyaltyCustomer(LoyaltyCustomer customer);

        void DeleteLoyaltyCustomer(LoyaltyCustomer customer);

        LoyaltyCustomer GetLoyaltyCustomer(Guid customerId);

        IEnumerable<LoyaltyCustomer> GetAllLoyaltyCustomers();

    }
}