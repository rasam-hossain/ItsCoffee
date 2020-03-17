using ItsCoffee.Core.Entities;

namespace ItsCoffee.Core.Services.OrderValidation
{
    public interface IValidatePartOfAnOrder
    {
        OrderValidationResult Validate(Order order);
    }
}