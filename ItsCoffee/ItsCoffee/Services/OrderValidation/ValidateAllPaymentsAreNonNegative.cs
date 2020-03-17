using System.Linq;
using ItsCoffee.Core.Entities;

namespace ItsCoffee.Core.Services.OrderValidation
{
    public class ValidateAllPaymentsAreNonNegative : IValidatePartOfAnOrder
    {
        public OrderValidationResult Validate(Order order)
        {
            if(order.Payments.paymentAmounts.Any(payment => payment.Value < 0))
                return new OrderValidationResult.FailedResult("Negative payment amounts are not allowed.");

            return new OrderValidationResult.SuccessfulResult();
        }
    }
}