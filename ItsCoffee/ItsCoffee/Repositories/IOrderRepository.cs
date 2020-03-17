using System;
using System.Collections.Generic;
using ItsCoffee.Core.Entities;

namespace ItsCoffee.Core.Repositories
{
    public interface IOrderRepository
    {
        Order GetOrder(Guid orderID);
        void AddOrder(Order order);
        void UpdateOrder(Order order);
        void RemoveOrder(Order order);
        IEnumerable<Order> GetAllOrders();
    }
}