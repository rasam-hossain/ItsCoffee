using System;
using System.Collections.Generic;
using System.Linq;
using ItsCoffee.Core.Entities;
using ItsCoffee.Core.Services.OrderValidation;

namespace ItsCoffee.Core.Services
{
    public class OrderValidator : IValidate<Order>
    {
        private readonly IEnumerable<IValidatePartOfAnOrder> _orderPartValidators;

        public OrderValidator(IEnumerable<IValidatePartOfAnOrder> orderPartValidators)
        {
            _orderPartValidators = orderPartValidators ?? throw new ArgumentNullException(nameof(orderPartValidators));
        }

        public IEnumerable<string> GetValidationMessages(Order order)
        {
            return _orderPartValidators
                .Select(validator => validator.Validate(order))
                .OfType<OrderValidationResult.FailedResult>()
                .Select(result => result.ValidationMessages)
                .SelectMany(messages => messages);
        }

        public bool IsValid(Order order)
        {
            return !GetValidationMessages(order).Any();
        }

    }
}