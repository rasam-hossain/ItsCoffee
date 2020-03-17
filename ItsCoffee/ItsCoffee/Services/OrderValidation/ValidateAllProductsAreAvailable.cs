using System.Collections.Generic;
using System.Linq;
using ItsCoffee.Core.Entities;

namespace ItsCoffee.Core.Services.OrderValidation
{
    public class ValidateAllProductsAreAvailable : IValidatePartOfAnOrder
    {
        public OrderValidationResult Validate(Order order)
        {
            List<string> messages = new List<string>();
            messages.AddRange(order.OrderItems.Where(item => !item.Product.IsAvailable).Select(item => $"{item.Product.Name} is currently unavailable."));

            if(messages.Any())
                return new OrderValidationResult.FailedResult(messages);

            return new OrderValidationResult.SuccessfulResult();
        }
    }
}