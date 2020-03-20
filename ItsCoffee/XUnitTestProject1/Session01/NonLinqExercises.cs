using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices.ComTypes;
using ItsCoffee.Core.Entities;
using Xunit;
using Xunit.Abstractions;

namespace ItsCoffee.Core.Tests
{
    /// <summary>
    /// Some exercises to do without linq
    /// - you can output to the test runner with:
    ///     - _output.WriteLine(--- some text here ---)
    /// - if you create a collection of strings and you want to output it, you can use:
    ///     - OutputCollection(--- collection of strings ---)
    /// </summary>
    public class NonLinqExercises : LinqTestsBase
    {
        /// <summary>
        /// Find all customers with the last name Fox
        /// </summary>
        [Fact]
        public void FindAllCustomersWithSpecificLastName()
        {
            var loyaltyCustomers = GetAllLoyaltyCustomers();
        }

        /// <summary>
        /// Find all customers that are likely duplicates
        /// - define a duplicate as having the same last name and first name, ignoring case
        /// </summary>
        [Fact]
        public void FindCustomersThatAreLikelyDuplicates()
        {
            var customers = GetAllLoyaltyCustomers();
        }

        /// <summary>
        /// Get the number of loyalty customers with a specific reward status
        /// </summary>
        /// <param name="status"></param>
        [Theory]
        [InlineData(RewardStatus.None)]
        [InlineData(RewardStatus.Silver)]
        [InlineData(RewardStatus.Gold)]
        [InlineData(RewardStatus.Diamond)]
        [InlineData(RewardStatus.Platinum)]
        public void GetNumberOfLoyaltyCustomersWithSpecificStatus(RewardStatus status)
        {
            var customers = GetAllLoyaltyCustomers();
        }

        /// <summary>
        /// Get the number of loyalty customers with each status
        /// </summary>
        [Fact]
        public void GetNumberOfLoyaltyCustomersWithEachStatus()
        {
            var statuses = (RewardStatus[])Enum.GetValues(typeof(RewardStatus));
            var loyaltyCustomers = GetAllLoyaltyCustomers();
        }

        /// <summary>
        /// Get the combined number of lifetime loyalty points handed out to all customers collectively
        /// </summary>
        [Fact]
        public void GetTotalLifetimeLoyaltyPointsForAllLoyaltyCustomers()
        {
            var customers = GetAllLoyaltyCustomers();
        }

        /// <summary>
        /// Get the 5 most frequent customers
        /// - define frequency as number of orders
        /// </summary>
        [Fact]
        public void Get5MostFrequentCustomers()
        {
            var orders = GetAllOrders();
        }

        /// <summary>
        /// Get 5 highest lifetime loyalty points earners
        /// </summary>
        [Fact]
        public void Get5AllTimeHighestRewardEarners()
        {
            var customers = GetAllLoyaltyCustomers();
        }

        /// <summary>
        /// Get 5 highest reward spenders
        /// </summary>
        [Fact]
        public void Get5AllTimeHighestRewardSpenders()
        {
            var customers = GetAllLoyaltyCustomers();
        }

        /// <summary>
        /// Find any products that are unavailable
        /// </summary>
        [Fact]
        public void FindAnyProductsThatAreUnavailable()
        {
            var products = GetAllProducts();
        }

        /// <summary>
        /// Get unique customers from the Orders table and count them
        /// </summary>
        [Fact]
        public void GetAllTheUniqueCustomersOnOrdersAndCountThem()
        {
            var orders = GetAllOrders();
        }

        /// <summary>
        /// Find customers that have never spent any loyalty points
        /// </summary>
        [Fact]
        public void FindCustomersThatHaveNeverSpentAnyLoyaltyPoints()
        {
            var customers = GetAllLoyaltyCustomers();        }

        private readonly ITestOutputHelper _output;
        public NonLinqExercises (ITestOutputHelper output)
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
