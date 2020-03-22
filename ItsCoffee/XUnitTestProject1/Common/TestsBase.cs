using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using Dapper;
using ItsCoffee.Core.Entities;
using ItsCoffee.Core.Repositories;
using ItsCoffee.Core.Services;
using ItsCoffee.Core.Services.CouponValidation;
using ItsCoffee.Core.Services.OrderValidation;

namespace ItsCoffee.Core.Tests
{
    public class TestsBase : IDisposable
    {
        public IDbConnection _testDbConnection { get; set; }
        private static Random random = new Random();

        public TestsBase()
        {
            _testDbConnection = new SQLiteConnection(@"Data Source=.\ItsCoffee.db;Version=3;");
            
            SqlMapper.AddTypeHandler(new GuidTypeHandler());
            SqlMapper.RemoveTypeMap(typeof(Guid));
            SqlMapper.RemoveTypeMap(typeof(Guid?));
        }

        protected static Product GetValidProduct()

        {
            var product = new Product();
            product.Name = GetRandomString();
            product.AvailableSizes = new List<ProductSize>(){ProductSize.Medium};
            product.Categories = new List<ProductCategory>(){ProductCategory.HotCoffee, ProductCategory.ColdCoffee};
            product.IsAvailable = true;
            return product;
        }

        protected OrderService OrderService => new OrderService(new OrderRepository(_testDbConnection),
                GetHandleOrderProcessedEvents(_testDbConnection),
                OrderValidator,
                new CouponRepository(_testDbConnection),
                new CouponValidator(
                    new List<IValidateCoupon> { new ValidateCouponCodeAvailable()},
                    new CouponRepository(_testDbConnection))
                );

        private CouponValidator CouponValidator => new CouponValidator(new List<IValidateCoupon>
        {
            new ValidateCouponCodeIsUnique()
        }, new CouponRepository(_testDbConnection));

        protected CouponService CouponService => new CouponService(new CouponRepository(_testDbConnection), CouponValidator);
        private OrderValidator OrderValidator => new OrderValidator( new List<IValidatePartOfAnOrder> 
            {
                new ValidatePartOfUniqueIdentifier(), 
                new ValidateAtLeastOneItem(), 
                new ValidateAllProductsAreAvailable(), 
                new ValidateAllPaymentsAreNonNegative(),
                new ValidateConsumedLoyaltyPointsAreLessThanBalance(),
                new ValidateThereIsAtLeastOnePayment()
            });

        private static string GetRandomString()
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            int length = 8;
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }


        protected static LoyaltyCustomer GetValidLoyaltyCustomer()
        {
            var loyaltyCustomer = new LoyaltyCustomer()
            {
                CustomerId = Guid.NewGuid(),
                EmailAddress = "test@test.com",
                FirstName = "test",
                LastName = "test",
                RewardStatus = RewardStatus.Gold,
                LoyaltyPointsBalance = 1.0m,
                LifetimeLoyaltyPoints = 1.0m
            };
            return loyaltyCustomer;
        }  
        
        protected static Order GetValidOrder()
        {
            var order = new Order();
            
             order.AddLoyaltyCustomerToOrder(GetValidLoyaltyCustomer());
             order.AddItemToOrder(new OrderItem(GetValidProduct(), ProductSize.Medium,2.5m));
             return order;
        }

        protected static HandleOrderProcessedEvent GetHandleOrderProcessedEvents(IDbConnection _testDbConnection)
        {
            var handleOrderProcessedEvents = new HandleOrderProcessedEvent();
            handleOrderProcessedEvents.OrderProcessed += new LoyaltyService(new LoyaltyRepository(_testDbConnection)).ProcessLoyaltyPoints;
            return handleOrderProcessedEvents;
        }

        public void Dispose()
        {
            _testDbConnection.Close();
            _testDbConnection.Dispose();
        }
    }
}