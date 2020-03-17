using System;
using System.Data;
using ItsCoffee.Core.Entities;

namespace ItsCoffee.Core.Services.OrderValidation
{
    public class ValidatePartOfUniqueIdentifier : IValidatePartOfAnOrder
    {
        public OrderValidationResult Validate(Order order)
        {
            if(order.OrderId.Equals(Guid.Empty))
                return new OrderValidationResult.FailedResult("An order must have a unique identifier.");

            return new OrderValidationResult.SuccessfulResult();
        }
    }
}