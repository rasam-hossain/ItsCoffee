using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using ItsCoffee.Core.Entities;

namespace ItsCoffee.Core.Repositories
{
    public class OrderRepository : IOrderRepository
    {

        private readonly IDbConnection _db;

        public OrderRepository(IDbConnection db)
        {
            _db = db ?? throw new ArgumentNullException(nameof(db));
        }

        public Order GetOrder(Guid orderID)
        {
            var multi = _db.QueryMultiple("SELECT * FROM 'Order' where OrderId = @orderId;"
                                          + " SELECT * FROM OrderItem WHERE OrderId = @OrderId;",
                new
                {
                    OrderId = orderID
                });
            Order order = multi.Read<Order>().FirstOrDefault();
            if (order != null)
            {
                order.OrderItems = multi.Read<OrderItem>().ToList();
            }

            return order;
        }
        public IEnumerable<Order> GetAllOrders()
        {
            var sql = @"SELECT *
                        FROM
                            'Order' O
                        INNER JOIN
                            LoyaltyCustomer L ON
                            O.LoyaltyCustomerId = L.CustomerId;";

            var orders = _db.Query<Order, LoyaltyCustomer, Order>(
                sql,
                (order, customer) =>
                {
                    var orderEntry = order;
                    orderEntry.AddLoyaltyCustomerToOrder(customer);
                    return orderEntry;
                },
                splitOn: "LoyaltyCustomerId");

            return orders;
        }

        public void AddOrder(Order order)
        {
            _db.Open();
            using (var transaction = _db.BeginTransaction())
            {
                var sql = "INSERT INTO 'Order' (OrderId, LoyaltyCustomerId) VALUES (@OrderId, @LoyaltyCustomerId);";

               _db.Execute(sql, new
               {
                   OrderId = order.OrderId,

                   // Todo - Test 7 - Used the null conditional to get rid of the NullReferenceException (https://csharp.today/c-6-features-null-conditional-and-and-null-coalescing-operators/)
                   LoyaltyCustomerId = order.LoyaltyCustomer?.CustomerId 

               });

                var sqlItems = "INSERT INTO 'OrderItem' (OrderId, ProductId, Price, CouponId) VALUES (@OrderId, @ProductId, @Price, @CouponId)";
                foreach (OrderItem item in order.OrderItems)
                {
                    _db.Execute(sqlItems, new
                    {
                        OrderId = order.OrderId,
                        ProductId = item.Product.ProductId,
                        Price = item.Price,
                        CouponId = order.Coupon.CouponID
                    });
                }

                transaction.Commit();

            }
            _db.Close();

        }

        public void UpdateOrder(Order order)
        {
            throw new NotImplementedException();
        }

        public void RemoveOrder(Order order)
        {
            throw new NotImplementedException();
        }

    }
}