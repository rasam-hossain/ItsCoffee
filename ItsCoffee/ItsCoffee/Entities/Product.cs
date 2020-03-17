using System.Collections.Generic;

namespace ItsCoffee.Core.Entities
{
    public class Product
    {
        public int ProductId { get; set; }
        public string Name { get; set; }
        public IEnumerable<ProductSize> AvailableSizes { get; set; }
        public bool IsAvailable { get; set; }
        public IEnumerable<ProductCategory> Categories { get; set; }

        public Product()
        {
            AvailableSizes = new List<ProductSize>();
            Categories = new List<ProductCategory>();
        }

    }
}
