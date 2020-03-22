using ItsCoffee.Core.Entities;
using ItsCoffee.Core.Repositories;
using ItsCoffee.Core.Services.CouponValidation;

namespace ItsCoffee.Core.Services
{
    public interface IValidateCoupon
    {
        CouponValidationResult Validate(Coupon coupon, CouponRepository couponRepository);
    }
}