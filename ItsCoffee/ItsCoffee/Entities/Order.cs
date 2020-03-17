using System;
using System.Collections.Generic;
using System.Linq;

namespace ItsCoffee.Core.Entities
{
    public class Order
    {
        public Guid OrderId { get; set; }
        public Coupon Coupon { get; private set; }
        public IList<OrderItem> OrderItems { get; set; }

        public LoyaltyCustomer LoyaltyCustomer { get; private set; }

        public Payments Payments { get; private set; }
        public decimal? OrderTotal { get; set; }

        public Order(Guid orderId) : this()
        {
            OrderId = orderId;
        }
        public Order()
        {
            OrderId = Guid.NewGuid();
            Coupon = new Coupon();
            OrderItems = new List<OrderItem>();
            Payments = new Payments();
        }
        public void AddItemToOrder(OrderItem orderItem)
        {
            OrderItems.Add(orderItem);
        }

        public void AddCouponToOrder(Coupon coupon)
        {
            Coupon = coupon;
        }
        public void AddLoyaltyCustomerToOrder(LoyaltyCustomer customer)
        {
            LoyaltyCustomer = customer;
        }
        public void RemoveItemFromOrder(OrderItem orderItem)
        {
            OrderItems.Remove(orderItem);
        }

        public decimal GetEarnedLoyaltyPointsBaseAmount()
        {
            // Todo - Test 8 - Return base amount of loyalty points
            // Calculated as 10% of the payments that were not paid using loyalty points
            return Payments.paymentAmounts.Where(payment => payment.Key != PaymentType.LoyaltyPoints).Sum(payment => payment.Value) * 0.1m;
        }
    }
}
