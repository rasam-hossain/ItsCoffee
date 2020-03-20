using System.Collections.Generic;
using ItsCoffee.Core.Entities;
using ItsCoffee.Core.Repositories;

namespace ItsCoffee.Core.Tests
{
    public class LinqTestsBase : TestsBase
    {

        public IEnumerable<LoyaltyCustomer> GetAllLoyaltyCustomers()
        {
            var loyaltyRepo = new LoyaltyRepository(_testDbConnection);
            return loyaltyRepo.GetAllLoyaltyCustomers();
        }

        public IEnumerable<Order> GetAllOrders()
        {
            var orderRepo = new OrderRepository(_testDbConnection);
            return orderRepo.GetAllOrders();
        }

        public IEnumerable<Product> GetAllProducts()
        {
            var productRepo = new ProductRepository(_testDbConnection);
            return productRepo.GetAllProducts();
        }
    }
}