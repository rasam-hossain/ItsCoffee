using System;
using System.Collections.Generic;
using System.Linq;
using ItsCoffee.Core.Entities;

namespace ItsCoffee.Core.Services
{
    public class ProductValidatorBase
    {
        public IEnumerable<string> GetValidationMessagesBase(Product product)
        {
            if (string.IsNullOrWhiteSpace(product.Name))
            {
                yield return "Product must have a name.";
            }

            if (!product.AvailableSizes.Any())
            {
                yield return "Product must have at least one size.";
            }

            if (!product.Categories.Any())
            {
                yield return "Product must have at least one category.";
            }
        }

    }
}