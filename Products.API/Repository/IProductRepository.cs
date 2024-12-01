using Products.API.model;

namespace Products.API.Repository
{
    public interface IProductRepository
    {
        List<Product> GetProductsList();
    }
}
