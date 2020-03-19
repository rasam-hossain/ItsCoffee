using System;
using System.Collections.Generic;
using System.Text;

namespace ItsCoffee.Core.Services.CouponValidation
{
    public abstract class CouponValidationResult
    {
        public class SuccessfulResult : CouponValidationResult
        {

        }
        public class FailedResult : CouponValidationResult
        {
            public IEnumerable<string> ValidationMessages;
            public FailedResult(string validationMessage)
            {
                ValidationMessages = new[]
                {
                    validationMessage
                };
            }
            public FailedResult(IEnumerable<string> validationMessages)
            {
                ValidationMessages = validationMessages;
            }
        }
    }
}
