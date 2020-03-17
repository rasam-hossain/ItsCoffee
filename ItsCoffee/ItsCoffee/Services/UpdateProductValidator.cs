using System.Collections.Generic;
using System.Linq;
using ItsCoffee.Core.Entities;

namespace ItsCoffee.Core.Services
{
    public class UpdateProductValidator : ProductValidatorBase, IValidate<Product>
    {
        
        public IEnumerable<string> GetValidationMessages(Product product)
        {
           return base.GetValidationMessagesBase(product);
        }

        public bool IsValid(Product product)
        {
            return !GetValidationMessages(product).Any();
        }

    }
}