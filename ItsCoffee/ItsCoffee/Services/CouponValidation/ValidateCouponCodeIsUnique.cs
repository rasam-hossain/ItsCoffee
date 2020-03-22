using ItsCoffee.Core.Entities;
using ItsCoffee.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace ItsCoffee.Core.Services.CouponValidation
{
    public class ValidateCouponCodeIsUnique : IValidateCoupon
    {
        public CouponValidationResult Validate(Coupon coupon, CouponRepository couponRepository)
        {
            if (couponRepository.GetCoupon(coupon.CouponCode) != null)
            {
                return new CouponValidationResult.FailedResult($"Coupon code: {coupon.CouponCode} already exists");
            }
            else
                return new CouponValidationResult.SuccessfulResult();
        }
    }
}
