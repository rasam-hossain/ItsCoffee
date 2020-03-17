using System.Linq;
using ItsCoffee.Core.Entities;

namespace ItsCoffee.Core.Services.OrderValidation
{
    public class ValidateConsumedLoyaltyPointsAreLessThanBalance : IValidatePartOfAnOrder
    {
        public OrderValidationResult Validate(Order order)
        {
            if(order.Payments.paymentAmounts.Where(payment => payment.Key == PaymentType.LoyaltyPoints).Any(payment => payment.Value > order.LoyaltyCustomer.LoyaltyPointsBalance))
                return new OrderValidationResult.FailedResult("Trying to use more loyalty points than are available.");

            return new OrderValidationResult.SuccessfulResult();
        }
    }
}