using Microsoft.AspNetCore.Mvc;
using Products.API.model;
using Products.API.Repository;

namespace Products.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly IProductRepository _repository;

        public ProductController(IProductRepository productRepository)
        {
            _repository = productRepository;
        }

        [HttpGet]
        public IEnumerable<Product> Get()
        {
            return _repository.GetProductsList();
        }
    }
}
