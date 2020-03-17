using System.Linq;
using ItsCoffee.Core.Entities;

namespace ItsCoffee.Core.Services.OrderValidation
{
    public class ValidateThereIsAtLeastOnePayment : IValidatePartOfAnOrder
    {
        public OrderValidationResult Validate(Order order)
        {
            if(order.Payments.paymentAmounts.Any())
                return new OrderValidationResult.SuccessfulResult();

            return new OrderValidationResult.FailedResult("Order must have at least one payment");
        }
    }
}