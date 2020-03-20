using System.Collections.Generic;
using ItsCoffee.Core.Entities;
using ItsCoffee.Core.Repositories;

namespace ItsCoffee.Core.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository productRepository;

        public ProductService(IProductRepository repository)
        {
           this.productRepository = repository;
        }
        public Product GetProduct(int productId)
        {
            return productRepository.GetProduct(productId);
        } 
        public void AddProduct(Product product)
        {
            if(new AddProductValidator().IsValid(product))
                productRepository.AddProduct(product);
        }

        public void UpdateProduct(Product product)
        {
            if(new UpdateProductValidator().IsValid(product))
                productRepository.UpdateProduct(product);
        } 
        
        public void RemoveProduct(Product product)
        {
            productRepository.RemoveProduct(product);
        }
        public List<Product> GetAllProducts()
        {
            return productRepository.GetAllProducts();
        }

        public List<Product> SearchProducts(string searchTerm)
        {
            return productRepository.SearchProducts(searchTerm);
        }
    }
}
