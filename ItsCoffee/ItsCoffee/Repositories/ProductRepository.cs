using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using ItsCoffee.Core.Entities;
using ItsCoffee.Core.Exceptions;

namespace ItsCoffee.Core.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly IDbConnection _db;

        public ProductRepository(IDbConnection db)
        {
            _db = db ?? throw new ArgumentNullException(nameof(db));
        }

        public Product GetProduct(int productId)
        {
            var product = _db.Query<Product>("SELECT * FROM Product where ProductId = @productId;",
                    new
                    {
                        ProductId = productId
                    })
                .ToList()
                .FirstOrDefault();
            if (product != null)
            {
                product.AvailableSizes = _db.Query<ProductSize>("SELECT * FROM ProductAvailableSizes where ProductId = @ProductId;",
                        new
                        {
                            ProductId = productId
                        })
                    .ToList();

                product.Categories = _db.Query<ProductCategory>("SELECT * FROM ProductCategories where ProductId = @ProductId;",
                        new
                        {
                            ProductId = productId
                        })
                    .ToList();
            }
            else
            {
                throw new NotFoundException("Product Not Found");
            }


            return product;
        }

        public void AddProduct(Product product)
        {
            _db.Open();
            using (var transaction = _db.BeginTransaction())
            {
                var sql = "INSERT INTO Product (Name, IsAvailable) Values (@Name, @IsAvailable);" +
                          "SELECT last_insert_rowid();";

                var productId = _db.QuerySingle<int>(sql, product);

                var sqlSizes = "INSERT INTO ProductAvailableSizes VALUES (@ProductId, @ProductSize)";
                foreach (ProductSize size in product.AvailableSizes)
                {
                    _db.Execute(sqlSizes, new
                    {
                        ProductId = productId,
                        ProductSize = size.ToString()
                    });
                } 
                
                var sqlCategories = "INSERT INTO ProductCategories VALUES (@ProductId, @ProductCategory)";
                foreach (ProductCategory productCategory in product.Categories)
                {
                    _db.Execute(sqlCategories, new
                    {
                        ProductId = productId,
                        ProductCategory = productCategory.ToString()
                    });
                }

                transaction.Commit();

            }
            _db.Close();
        }

        public void UpdateProduct(Product product)
        {
            throw new NotImplementedException();
        }

        public void RemoveProduct(Product product)
        {
            var sql = "DELETE FROM Product WHERE ProductId = @ProductId;";

            _db.Execute(sql, new
            {
                ProductId = product.ProductId
            });
        }

        public List<Product> GetAllProducts()
        {
            return _db.Query<Product>("SELECT * FROM Product;").ToList();
        }

        public List<Product> SearchProducts(string searchTerm)
        {
            var searchProducts = _db.Query<Product>("SELECT * FROM Product where Name like @Value;",
                    new
                    {
                        Value = "%" + searchTerm + "%"
                    })
                .ToList();
            return searchProducts;
        }
    }
}