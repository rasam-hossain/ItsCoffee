using System;
using System.Collections.Generic;
using System.Text;

namespace ItsCoffee.Core.Entities
{
    public class Coupon
    {
        public Guid CouponID { get; set; }
        public string CouponCode { get; set; }
        public CouponType CouponType { get; set; }
        public int? DiscountAmount { get; set; }
        public Guid OrderId { get; set; }
        public Coupon()
        {
            CouponID = Guid.NewGuid();
            CouponCode = null;
            CouponType = CouponType.None;
            DiscountAmount = 0;
        }

        public Coupon(Guid couponID, string couponCode, CouponType couponType, int? discountAmount)
        {
            CouponID = couponID;
            CouponCode = couponCode;
            CouponType = couponType;
            DiscountAmount = discountAmount;
        }
    }
    public enum CouponType
    {
        None,
        Percentage,
        FlatRate
    }
}
