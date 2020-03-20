﻿using ItsCoffee.Core.Entities;
using ItsCoffee.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace ItsCoffee.Core.Services
{
    public class CouponService
    {
        private readonly ICouponRepository _couponRepository;
        public CouponService(ICouponRepository couponRepository)
        {
            _couponRepository = couponRepository;
        }
        public void ApplyCouponToOrder(Order order)
        {
            switch (order.Coupon.CouponType)
            {
                case CouponType.Percentage:
                    ApplyDiscountOnOrder(order);
                    break;
                case CouponType.FlatRate:
                    ApplyFlatRateOnOrder(order);
                    break;
                default:
                    break;
            }
        }

        private void ApplyDiscountOnOrder(Order order)
        {
            order.OrderTotal = 0m;
            foreach (OrderItem orderItem in order.OrderItems)
            {
                order.OrderTotal += orderItem.Price - (order.Coupon.DiscountAmount / 100.00m);
            }
        }

        private void ApplyFlatRateOnOrder(Order order)
        {
            if (order.OrderTotal > order.Coupon.DiscountAmount)
                order.OrderTotal -= order.Coupon.DiscountAmount;
        }
        public void AddCoupon(Order order)
        {
            _couponRepository.AddCoupon(order);
        }
        public void UpdateCoupon(Coupon coupon)
        {
            _couponRepository.UpdateCoupon(coupon);
        }
        public void RemoveCoupon(Coupon coupon)
        {
            _couponRepository.RemoveCoupon(coupon);
        }
        public void GetAllCoupons()
        {
            _couponRepository.GetAllCoupons();
        }
        public Coupon GetCoupon(string coupon)
        {
            return _couponRepository.GetCoupon(coupon);
        }
    }
}
