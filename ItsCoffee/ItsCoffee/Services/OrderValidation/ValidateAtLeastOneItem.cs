using System.Linq;
using ItsCoffee.Core.Entities;

namespace ItsCoffee.Core.Services.OrderValidation
{
    public class ValidateAtLeastOneItem : IValidatePartOfAnOrder
    {
        public OrderValidationResult Validate(Order order)
        {
            if(order.OrderItems.Any())
                return new OrderValidationResult.SuccessfulResult();

            return new OrderValidationResult.FailedResult("An order must contain at least one item.");
        }
    }
}