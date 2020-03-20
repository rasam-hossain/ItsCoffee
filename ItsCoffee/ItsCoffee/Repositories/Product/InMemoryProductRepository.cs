using System.Collections.Generic;
using System.Linq;
using ItsCoffee.Core.Entities;
using ItsCoffee.Core.Exceptions;

namespace ItsCoffee.Core.Repositories
{
    public class InMemoryProductRepository : IProductRepository
    {

        public Dictionary<int, Product> Products { get; set; }

        public InMemoryProductRepository()
        {
            Products = new Dictionary<int, Product>();
        }

        public Product GetProduct(int productId)
        {
            return Products.ContainsKey(productId) ? Products[productId] : throw new NotFoundException("Product not found.");
        }

        public void AddProduct(Product product)
        {
            product.ProductId = Products.Count + 1;
            Products[product.ProductId] = product;
        }

        public void UpdateProduct(Product product)
        {

            Products[product.ProductId] = product;
        }

        public void RemoveProduct(Product product)
        {
            Products.Remove(product.ProductId);
        }

        public List<Product> GetAllProducts()
        {
            return new List<Product>(Products.Values);
        }

        public List<Product> SearchProducts(string searchTerm)
        {
            return Products.Values.Where(x => x.Name.Contains(searchTerm)).ToList();
        }
    }
}