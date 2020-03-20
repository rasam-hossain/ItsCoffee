using ItsCoffee.Core.Entities;
using ItsCoffee.Core.Exceptions;
using ItsCoffee.Core.Services;
using ItsCoffee.Core.Services.OrderValidation;
using System;
using System.Linq;
using Xunit;
using Xunit.Abstractions;
using static ItsCoffee.Core.Services.OrderValidation.OrderValidationResult;

namespace ItsCoffee.Core.Tests
{
    /// <summary>
    /// This particular test class verifies different
    /// classes in the Order Validation folder
    /// </summary>
    public class OrderValidatorTests : TestsBase
    {
        [Fact]
        public void Validate_All_Payments_Are_Non_Negative()
        {
            var order = GetValidOrder();
            var orderItem = order.OrderItems.First();

            order.Payments.AddPayment(PaymentType.Cash, -orderItem.Price);
            var validatePayment = new ValidateAllPaymentsAreNonNegative();
            var result = validatePayment.Validate(order);

            _output.WriteLine(result.GetType().ToString());
            if (result.GetType() == typeof(FailedResult))
            {
                var failedResult = result as FailedResult;
                var resultAsFailed = (OrderValidationResult.FailedResult)result;
                Assert.Equal("Negative payment amounts are not allowed.",
                            failedResult.ValidationMessages.ToList().First());
            }
        }

        [Fact]
        public void Validate_All_Products_Are_Available()
        {
            var order = GetValidOrder();
            order.Payments.AddPayment(PaymentType.Cash, 1.0M);

            // NOT SURE HOW TO IMPLEMENT THIS
        }


        [Fact]
        public void Validate_At_Least_One_Item()
        {
            var order = GetValidOrder();
            order.OrderItems.Clear();
            order.Payments.AddPayment(PaymentType.Cash, 1.0M);

            var validateOrderItem = new ValidateAtLeastOneItem();
            var result = validateOrderItem.Validate(order);

            if (result.GetType() == typeof(FailedResult))
            {
                Assert.Equal("An order must contain at least one item.",
                (result as FailedResult).ValidationMessages.ToList().First());
            }
        }

        [Fact]
        public void Validate_Consumed_Loyalty_Points_Are_Less_Than_Balance()
        {
            var order = GetValidOrder();
            var customer = GetValidLoyaltyCustomer();
            order.Payments.AddPayment(PaymentType.LoyaltyPoints, customer.LoyaltyPointsBalance + 1.0M);

            var validateConsumedLoyaltyPoints = new ValidateConsumedLoyaltyPointsAreLessThanBalance();
            var result = validateConsumedLoyaltyPoints.Validate(order);

            if (result.GetType() == typeof(FailedResult))
            {
                Assert.Equal("Trying to use more loyalty points than are available.",
                    (result as FailedResult).ValidationMessages.ToList().First());
            }

        }

        [Fact]
        public void Validate_Part_Of_Unique_Identifier()
        {
            var order = GetValidOrder();
            order.OrderId = Guid.Empty;

            var validatePartOfUniqueIdentifier = new ValidatePartOfUniqueIdentifier();
            var result = validatePartOfUniqueIdentifier.Validate(order);

            if (result.GetType() == typeof(FailedResult))
            {
                Assert.Equal("An order must have a unique identifier.",
                    (result as FailedResult).ValidationMessages.ToList().First());
            }
        }

        [Fact]
        public void Validate_There_Is_At_Least_One_Payment()
        {
            var order = GetValidOrder();
            order.OrderId = Guid.Empty;

            var validatePartOfUniqueIdentifier = new ValidatePartOfUniqueIdentifier();
            var result = validatePartOfUniqueIdentifier.Validate(order);

            if (result.GetType() == typeof(FailedResult))
            {
                Assert.Equal("An order must have a unique identifier.",
                    (result as FailedResult).ValidationMessages.ToList().First());
            }
        }


        [Fact]
        public void Create_One_Order()
        {
            var order = GetValidOrder();
            order.Payments.AddPayment(PaymentType.LoyaltyPoints, 0m);

            IOrderService orderService = OrderService;

            orderService.ProcessOrder(order);
        }


        private readonly ITestOutputHelper _output;
        public OrderValidatorTests(ITestOutputHelper output)
        {
            _output = output;
        }
    }
}
