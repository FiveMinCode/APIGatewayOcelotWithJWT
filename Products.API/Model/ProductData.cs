using Products.API.model;

namespace Products.API.Model
{
    public static class ProductData
    {
        public static List<Product> Products = new List<Product>()
        {
            new Product
            {
                Id = 1,
                Name = "Rocker",
                Brand = "Nike",
                Category = "shoes",
                Price = 100,
            },
            new Product
            {
                Id = 2,
                Name = "vapor",
                Brand = "Nike",
                Category = "shoes",
                Price = 150,
            },
            new Product
            {
                Id = 3,
                Name = "ride",
                Brand = "Spikes",
                Category = "shoes",
                Price = 110,
            }
        };
    }
}
