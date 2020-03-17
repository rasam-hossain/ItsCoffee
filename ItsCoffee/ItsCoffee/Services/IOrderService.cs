using System;
using ItsCoffee.Core.Entities;

namespace ItsCoffee.Core.Services
{
    public interface IOrderService
    {
        void ProcessOrder(Order order);
        Order GetOrderById(Guid orderId);
    }
}
