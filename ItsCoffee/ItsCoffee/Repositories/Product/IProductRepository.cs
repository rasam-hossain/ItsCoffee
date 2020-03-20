using System.Collections.Generic;
using ItsCoffee.Core.Entities;

namespace ItsCoffee.Core.Repositories
{
    public interface IProductRepository
    {

        Product GetProduct(int productId);
        void AddProduct(Product product);
        void UpdateProduct(Product product);
        void RemoveProduct(Product product);
        List<Product> GetAllProducts();
        List<Product> SearchProducts(string searchTerm);
    }
}