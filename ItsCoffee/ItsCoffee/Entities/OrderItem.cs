namespace ItsCoffee.Core.Entities
{
    public class OrderItem
    {
        public Product Product { get; set; }
        public ProductSize ProductSize { get; set; }

        public decimal Price { get; set; }

        public OrderItem()
        {
            
        }

        public OrderItem(Product product, ProductSize productSize, decimal price)
        {
            Product = product;
            ProductSize = productSize;
            Price = price;
        }
    }
}
