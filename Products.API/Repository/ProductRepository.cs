using Products.API.model;
using Products.API.Model;

namespace Products.API.Repository
{
    public class ProductRepository : IProductRepository
    {
        public List<Product> GetProductsList()
        {
            return ProductData.Products;
        }
    }
}
