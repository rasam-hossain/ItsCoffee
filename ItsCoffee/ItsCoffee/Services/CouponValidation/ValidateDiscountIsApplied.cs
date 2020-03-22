using ItsCoffee.Core.Entities;
using ItsCoffee.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace ItsCoffee.Core.Services.CouponValidation
{
    public class ValidateDiscountIsApplied : IValidateCoupon
    {
        public CouponValidationResult Validate(Coupon coupon, CouponRepository couponRepository)
        {
            throw new NotImplementedException();
        }
    }
}
