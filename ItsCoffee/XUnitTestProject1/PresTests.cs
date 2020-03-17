using System;
using System.Collections.Generic;
using System.Linq;
using ItsCoffee.Core.Entities;
using Xunit;
using Xunit.Abstractions;

namespace ItsCoffee.Core.Tests
{
    public class PresTests : LinqTestsBase
    {
        [Fact]
        public void DemoSomeStuff()
        {
            IEnumerable<LoyaltyCustomer> customers = GetAllLoyaltyCustomers();
            IEnumerable<Order> orders = GetAllOrders();
            IEnumerable<Product> products = GetAllProducts();

            // Select
//            IEnumerable<string> lastNames = customers.Select(loyaltycustomer => loyaltycustomer.LastName);
//            OutputCollection(lastNames);
//            var shortenedCustomers = customers.Select(loyaltycustomer => new { loyaltycustomer.FirstName, loyaltycustomer.LastName});
//            OutputCollection(shortenedCustomers.Select(sc => $"{sc.FirstName} {sc.LastName}"));
            // Where
            _output.WriteLine("Last name starts with d: ");
            var cWhereLNstartswithD = customers.Where(customer => Find(customer));
//                .Where(customer => customer.LastName.StartsWith('D'))
//                .Where(customer => customer.FirstName.StartsWith('S'));

            OutputCollection(cWhereLNstartswithD.Select(c => c.LastName));
            foreach (var loyaltyCustomer in cWhereLNstartswithD)
            {
                _output.WriteLine(loyaltyCustomer.ToString());
            }
            // Sum
            // OrderBy
            // Any

        }

        [Fact]
        public void DoSum()
        {
            IEnumerable<LoyaltyCustomer> customers = GetAllLoyaltyCustomers();

            decimal totalLoyaltyBalance =
                customers.Sum(customer => customer.LoyaltyPointsBalance);

            decimal newBalance =
                customers.Select(customer => customer.LoyaltyPointsBalance)
                .Sum();

            decimal total = 0.0m;
            foreach (var loyaltyCustomer in customers)
            {
                total += loyaltyCustomer.LoyaltyPointsBalance;
            }

            _output.WriteLine(totalLoyaltyBalance.ToString());

        }

        [Fact]
        public void DoAny()
        {
            IEnumerable<LoyaltyCustomer> customers = GetAllLoyaltyCustomers();

            bool anyResult = customers.Any(customer => customer.LastName.StartsWith('D'));
            var anyResult2 = customers.Where(customer => customer.LastName.StartsWith('D'));

            var anyResult3 = customers.Count(customer => customer.LastName.StartsWith('D'));


            bool result3 = false;
            foreach (var loyaltyCustomer in customers)
            {
                if (loyaltyCustomer.LastName.StartsWith('D'))
                    result3 = true;
            }

            _output.WriteLine($"Are there any that start with D: {anyResult}");
        }

        [Fact]
        public void DoGroupBy()
        {
            IEnumerable<LoyaltyCustomer> customers = GetAllLoyaltyCustomers();
            IEnumerable<Order> orders = GetAllOrders();

            IEnumerable<IGrouping<Guid, Order>> result 
                = orders.GroupBy(order => order.LoyaltyCustomer.CustomerId);

            foreach (var group in result)
            {
                _output.WriteLine($"{group.Key}: ");
                OutputCollection(group.Select(order => $"    {order.OrderId}"));
            }
        }



        private bool Find(LoyaltyCustomer customer)
        {
            return customer.LastName.StartsWith('D') &&
                   customer.FirstName.StartsWith('S');
        }
        
        private readonly ITestOutputHelper _output;
        public PresTests (ITestOutputHelper output)
        {
            _output = output;
        }

        private void OutputCollection(IEnumerable<string> collection)
        {
            foreach (var item in collection)
            {
                _output.WriteLine(item);
            }
        }
    }
}