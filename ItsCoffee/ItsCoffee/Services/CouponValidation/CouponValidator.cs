using ItsCoffee.Core.Entities;
using ItsCoffee.Core.Services.CouponValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ItsCoffee.Core.Services
{
    public class CouponValidator : IValidate<Coupon>
    {
        private readonly IEnumerable<IValidateCoupon> _couponValidators;

        public CouponValidator(IEnumerable<IValidateCoupon> couponValidators)
        {
            _couponValidators = couponValidators ?? throw new ArgumentNullException(nameof(couponValidators));
        }

        public IEnumerable<string> GetValidationMessages(Coupon coupon)
        {
            return _couponValidators
                .Select(validator => validator.Validate(coupon))
                .OfType<CouponValidationResult.FailedResult>()
                .Select(result => result.ValidationMessages)
                .SelectMany(messages => messages);
        }

        public bool IsValid(Coupon coupon)
        {
            return !GetValidationMessages(coupon).Any();
        }
    }
}
