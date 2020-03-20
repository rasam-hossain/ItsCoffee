using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ItsCoffee.Core.Entities;
using ItsCoffee.Core.Exceptions;

namespace ItsCoffee.Core.Services
{
    public class AddProductValidator : ProductValidatorBase, IValidate<Product>
    {
        

        public IEnumerable<string> GetValidationMessages(Product product)
        {
            List<string> messages = new List<string>();
            if (product.ProductId != 0)
            {
                messages.Add("A new product must not have an Identifier.");
            }

            messages.AddRange(base.GetValidationMessagesBase(product));
            return messages;
        }

        public bool IsValid(Product product)
        {
            return !GetValidationMessages(product).Any();
        }

    }
}