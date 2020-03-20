using System;
using System.Linq;
using ItsCoffee.Core.Entities;
using ItsCoffee.Core.Exceptions;
using ItsCoffee.Core.Repositories;
using ItsCoffee.Core.Services;
using Xunit;

namespace ItsCoffee.Core.Tests
{
    public class AssessmentTests : TestsBase
    {

        [Fact]
        public void Test_1_Should_Display_Points_Currency()
        {
            /*
             * 1) Ensure that we can format Loyalty Points using our currency
             */
            var customer = GetValidLoyaltyCustomer();
            Assert.Equal("☕1.00", customer.LoyaltyPointsBalance.ToLoyaltyString());
        }

        [Fact]
        public void Test_2_Should_Update_Customer_Name()
        {
            /*
            *
            * 1) Create a new Loyalty Customer and Add them
            * 2) Get the customer         
            * 3) Change the customer's first and last name
            * 4) Update the customer
            * 5) Get the customer
            * 6) Ensure the updated customer has the new information
            */
            var loyaltyService = new LoyaltyService(new LoyaltyRepository(_testDbConnection));

            const string firstName = "Test2";
            const string lastName = "2Test";
            var customer = new LoyaltyCustomer()
            {
                CustomerId = Guid.NewGuid(),
                EmailAddress = "test2@sgi.sk.ca",
                FirstName = firstName,
                LastName = lastName,
                LifetimeLoyaltyPoints = 0,
                LoyaltyPointsBalance = 0,
                RewardStatus = RewardStatus.Silver
            };
            loyaltyService.AddLoyaltyCustomer(customer);

            var newCustomer = loyaltyService.GetLoyaltyCustomer(customer.CustomerId);
            Assert.Equal(firstName, newCustomer.FirstName);
            Assert.Equal(lastName, newCustomer.LastName);

            newCustomer.FirstName = lastName;
            newCustomer.LastName = firstName;
            loyaltyService.UpdateLoyaltyCustomer(newCustomer);
            var updatedCustomer = loyaltyService.GetLoyaltyCustomer(customer.CustomerId);


            Assert.Equal(lastName, updatedCustomer.FirstName);
            Assert.Equal(firstName, updatedCustomer.LastName);
        }
        [Fact]
        public void Test3_Order_Should_Not_Accept_Negative_Payment()
        {
            /*
             *
             * 1) Create an order, add a payment of negative value
             * 2) Process order
             * 3) When processing the order we should expect a validation exception
             */

            var order = GetValidOrder();
            var orderItem = order.OrderItems.First();

            order.Payments.AddPayment(PaymentType.Cash, -orderItem.Price);

            IOrderService orderService = OrderService;
            Assert.Throws<ValidationException>(() => orderService.ProcessOrder(order));
        }


        [Fact]
        public void Test4_Order_Should_Not_Accept_More_LoyaltyPoints_Than_Customer_Balance()
        {
            /*
             *
             * 1) Create an order where the Loyalty Customer Doesn't have enough points to pay for all
             * 2) When processing the order we should expect a validation exception
             */

            var order = GetValidOrder();
            var orderItem = order.OrderItems.First();
            order.LoyaltyCustomer.LoyaltyPointsBalance = orderItem.Price - 0.5m;

            order.Payments.AddPayment(PaymentType.LoyaltyPoints, orderItem.Price);

            IOrderService orderService = OrderService;
            Assert.Throws<ValidationException>(() => orderService.ProcessOrder(order));
        }

        [Fact]
        public void Test5_Order_Should_Not_Sell_Product_When_Unavailable()
        {
            /*
             *
             * 1) Create an order and add a product
             * 2) Set a product to unavailable
             * 3) When processing the order we should expect a validation exception
             */

            var order = GetValidOrder();
            var orderItem = order.OrderItems.First();
            order.OrderItems.First().Product.IsAvailable = false;
            order.Payments.AddPayment(PaymentType.LoyaltyPoints, orderItem.Price);

            IOrderService orderService = OrderService;
            Assert.Throws<ValidationException>(() => orderService.ProcessOrder(order));
        }

        [Fact]
        public void Test6_Order_Should_Have_Product_and_Payment_To_Process()
        {
            /*
             *
             * 1) Create an order with no items
             * 2) When processing the order we should expect a validation exception
             * 3) Add an item to that order             
             * 4) When processing the order we should expect a validation exception
             * 5) Add a payment to that order
             */

            // Create an Order

            // Process order
            // Should fail

            var order = GetValidOrder();
            order.OrderItems.Clear();
            order.Payments.AddPayment(PaymentType.LoyaltyPoints, 0m);

            IOrderService orderService = OrderService;

            Assert.Throws<ValidationException>(() => orderService.ProcessOrder(order));

            order.AddItemToOrder(new OrderItem(GetValidProduct(), ProductSize.Medium, 2.5m));
            order.Payments.paymentAmounts.Clear();

            Assert.Throws<ValidationException>(() => orderService.ProcessOrder(order));
        }

        [Fact]
        public void Test7_Orders_with_no_loyaltycustomer_should_process()
        {

            /*
            *
            * 1) Create an order
            * 2) Add a product and a payment, but no customer          
            * 3) When processing the order we should expect no exception
            */

            var order = new Order();

            order.AddItemToOrder(new OrderItem(GetValidProduct(), ProductSize.Medium, 2.5m));
            order.Payments.AddPayment(PaymentType.Cash, order.OrderItems.First().Price);

            IOrderService orderService = OrderService;
            orderService.ProcessOrder(order);
        }

        [Fact]
        public void Test8_Orders_Should_Update_LoyaltyPoints()
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

            var customer = new LoyaltyCustomer()
            {
                CustomerId = Guid.NewGuid(),
                EmailAddress = "newuser@sgi.sk.ca",
                FirstName = "Test",
                LastName = "Test",
                LifetimeLoyaltyPoints = 0,
                LoyaltyPointsBalance = 0,
                RewardStatus = RewardStatus.Silver
            };
            loyaltyService.AddLoyaltyCustomer(customer);

            var order = new Order();
            order.AddLoyaltyCustomerToOrder(customer);
            const decimal price = 1m; // Buy a 1.00 product
            order.AddItemToOrder(new OrderItem(GetValidProduct(), ProductSize.Medium, price));
            order.Payments.AddPayment(PaymentType.Cash, price);

            orderService.ProcessOrder(order);

            var updatedCustomer = loyaltyService.GetLoyaltyCustomer(customer.CustomerId);
            Assert.Equal(price / 10, updatedCustomer.LoyaltyPointsBalance);
        }

        [Fact]
        public void Test9_Orders_Should_Update_LoyaltyPoints_When_Using_LoyaltyPoints()
        {
            /*
            *
            * 1) Create a new Loyalty Customer with 0 points and Add them
            * 2) Create a new Order and add the Loyalty Customer, a product and a payment          
            * 3) Process the order
            * 4) Get the customer
             * 5) Create a new order
             * 6) Add a product which costs as much as their points balance
             * 7) Pay for the new order with points
             * 8) Get the customer and ensure their balance is now 0
            *
            */

            var loyaltyService = new LoyaltyService(new LoyaltyRepository(_testDbConnection));
            IOrderService orderService = OrderService;


            var order = new Order();
            var customer = new LoyaltyCustomer()
            {
                CustomerId = Guid.NewGuid(),
                EmailAddress = "test9@sgi.sk.ca",
                FirstName = "Test",
                LastName = "Test",
                LifetimeLoyaltyPoints = 0,
                LoyaltyPointsBalance = 0,
                RewardStatus = RewardStatus.Silver
            };
            loyaltyService.AddLoyaltyCustomer(customer);
            order.AddLoyaltyCustomerToOrder(customer);
            const decimal price = 1000m; // Buy a $1000 product in order to get $100 worth of points 
            order.AddItemToOrder(new OrderItem(GetValidProduct(), ProductSize.Medium, price));
            order.Payments.AddPayment(PaymentType.Cash, price);

            orderService.ProcessOrder(order);

            var updatedCustomer = loyaltyService.GetLoyaltyCustomer(customer.CustomerId);

            order = new Order();
            order.AddLoyaltyCustomerToOrder(updatedCustomer);
            order.AddItemToOrder(new OrderItem(GetValidProduct(), ProductSize.Medium, price / 10));
            order.Payments.AddPayment(PaymentType.LoyaltyPoints, price / 10);
            orderService.ProcessOrder(order);

            updatedCustomer = loyaltyService.GetLoyaltyCustomer(customer.CustomerId);
            Assert.Equal(0, updatedCustomer.LoyaltyPointsBalance);
        }

        [Fact]
        public void Test10_LoyaltyStatus_Should_Update_When_Reaching_Milestone()
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
            const decimal price = 1001m; // Buy a $1001 product in order to become diamond status
            order.AddItemToOrder(new OrderItem(GetValidProduct(), ProductSize.Medium, price));
            order.Payments.AddPayment(PaymentType.Cash, price);

            IOrderService orderService = OrderService;

            orderService.ProcessOrder(order);

            var updatedCustomer = loyaltyService.GetLoyaltyCustomer(customer.CustomerId);

            Assert.Equal(RewardStatus.Diamond, updatedCustomer.RewardStatus);
        }


    }

}
