using System;
using ItsCoffee.Core.Entities;
using ItsCoffee.Core.Exceptions;
using ItsCoffee.Core.Repositories;

namespace ItsCoffee.Core.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly HandleOrderProcessedEvent _orderProcessedEvent;
        private readonly IValidate<Order> _orderValidator;
        private readonly ICouponRepository _couponRepository;
        private readonly IValidate<Coupon> _couponValidator;

        public OrderService(IOrderRepository orderRepository, HandleOrderProcessedEvent orderProcessedEvent, IValidate<Order> orderValidator, ICouponRepository couponRepository, IValidate<Coupon> couponValidator)
        {
            _orderRepository = orderRepository;
            _orderProcessedEvent = orderProcessedEvent;
            _orderValidator = orderValidator;
            _couponRepository = couponRepository;
            _couponValidator = couponValidator;
        }
        public void ProcessOrder(Order order)
        {
            if (!_orderValidator.IsValid(order))
            {
                throw new ValidationException(String.Join("\r\n", _orderValidator.GetValidationMessages(order)));
            }

           
            _orderRepository.AddOrder(order);

            // Event - order processed
            _orderProcessedEvent.OrderCompleted(order);
        }


        public Order GetOrderById(Guid orderId)
        {
           return _orderRepository.GetOrder(orderId);
        }
    }
}