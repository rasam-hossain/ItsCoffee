using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading;
using ItsCoffee.Core.Entities;
using ItsCoffee.Core.Exceptions;
using ItsCoffee.Core.Repositories;
using ItsCoffee.Core.Services;
using Xunit;

namespace ItsCoffee.Core.Tests
{
    public class AdditionalTests : TestsBase
    {

    /* The purpose of this class is as a place for you to write and keep any of your own tests, you may submit these or not */
    // Todo - Test 10 - Some additional tests to increase my confidence that I got the reward status calculation correct
        [Theory]
        [InlineData(11, RewardStatus.Silver)]
        [InlineData(201, RewardStatus.Gold)]
        [InlineData(1001, RewardStatus.Diamond)]
        [InlineData(2501, RewardStatus.Platinum)]
        public void Test10_LoyaltyStatus_Should_Update_When_Reaching_Milestone_AdditionalTests(decimal price, RewardStatus expectedStatus)
        {
            /*
            *
            * 1) Create a new Loyalty Customer with 0 points and Add them
            * 2) Create a new Order and add the Loyalty Customer, a product and a payment          
            * 3) Process the order
            * 4) Get the customer and ensure their status has changed appropriately
             *
             * */
            var order = new Order();
            var loyaltyService = new LoyaltyService(new LoyaltyRepository(_testDbConnection));

            var customer = new LoyaltyCustomer()
            {
                CustomerId = Guid.NewGuid(),
                EmailAddress = "test10@sgi.sk.ca",
                FirstName = "Test",
                LastName = "Test",
                LifetimeLoyaltyPoints = 0,
                LoyaltyPointsBalance = 0,
                RewardStatus = RewardStatus.Silver
            };
            loyaltyService.AddLoyaltyCustomer(customer);
            order.AddLoyaltyCustomerToOrder(customer);
            order.AddItemToOrder(new OrderItem(GetValidProduct(), ProductSize.Medium, price));
            order.Payments.AddPayment(PaymentType.Cash, price);

            IOrderService orderService = OrderService;
            orderService.ProcessOrder(order);

            var updatedCustomer = loyaltyService.GetLoyaltyCustomer(customer.CustomerId);

            Assert.Equal(expectedStatus, updatedCustomer.RewardStatus);
        }

        [Fact]
        public void AddSomeMoreOrdersForLoyaltyCustomer()
        {
            /*
          *
          * 1) Create a new Loyalty Customer with 0 points and Add them
          * 2) Create a new Order and add the Loyalty Customer, a product and a payment          
          * 3) Process the order
          * 4) Get the customer, and verify that their points were updated appropriately
          *
          */


            var loyaltyService = new LoyaltyService(new LoyaltyRepository(_testDbConnection));

            IOrderService orderService = OrderService;
            var customer = loyaltyService.GetLoyaltyCustomer(Guid.Parse("68df0eb2-cb07-42b9-870f-e2080314c73e"));

            var order = new Order();
            order.AddLoyaltyCustomerToOrder(customer);
            const decimal price = 1m; // Buy a 1.00 product
            order.AddItemToOrder(new OrderItem(GetValidProduct(), ProductSize.Medium, price));
            order.Payments.AddPayment(PaymentType.Cash, price);

            orderService.ProcessOrder(order);
        }


    }

}
