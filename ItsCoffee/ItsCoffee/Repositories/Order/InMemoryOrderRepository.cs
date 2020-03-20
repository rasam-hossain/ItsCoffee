using System;
using System.Collections.Generic;
using ItsCoffee.Core.Entities;
using ItsCoffee.Core.Exceptions;

namespace ItsCoffee.Core.Repositories
{
    public class InMemoryOrderRepository : IOrderRepository
    {

        public Dictionary<Guid, Order> Orders { get; set; }

        public InMemoryOrderRepository()
        {
            Orders = new Dictionary<Guid, Order>();
        }

        public Order GetOrder(Guid orderId)
        {
            return Orders.ContainsKey(orderId) ? Orders[orderId] : throw new NotFoundException("Order not found.");
        }

        public void AddOrder(Order order)
        {
            order.OrderId = Guid.NewGuid();
            Orders[order.OrderId] = order;
        }

        public void UpdateOrder(Order order)
        {
            throw new NotImplementedException();
        }

        public void RemoveOrder(Order order)
        {
            throw new NotImplementedException();
        }

        public void UpdateProduct(Order order)
        {

            Orders[order.OrderId] = order;
        }

        public void removeOrder(Order order)
        {
            Orders.Remove(order.OrderId);
        }

        public IEnumerable<Order> GetAllOrders()
        {
            return new List<Order>(Orders.Values);
        }
    }
}