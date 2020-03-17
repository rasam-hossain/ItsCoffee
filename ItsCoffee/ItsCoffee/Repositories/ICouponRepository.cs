using System;
using System.Collections.Generic;
using ItsCoffee.Core.Entities;

namespace ItsCoffee.Core.Repositories
{
    public interface ICouponRepository
    {
        void AddCoupon(Order order);
        void UpdateCoupon(Coupon coupon);
        void RemoveCoupon(Coupon coupon);
        IEnumerable<Coupon> GetAllCoupons();
        Coupon GetCoupon(string couponCode);
    }
}
