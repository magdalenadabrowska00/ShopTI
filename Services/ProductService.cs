using AutoMapper;
using Serilog;
using ShopTI.Entities;
using ShopTI.IServices;
using ShopTI.Models;

namespace ShopTI.Services
{
    public class ProductService : IProductService
    {
        private readonly ShopDbContext _dbcontext;
        private readonly IMapper _mapper;
        public ProductService(ShopDbContext dbcontext, IMapper mapper)
        {
            _dbcontext = dbcontext;
            _mapper = mapper;
        }

        public int CreateProduct(CreateProductModel newProduct)
        {
            var entity = _mapper.Map<Product>(newProduct);
            if (entity == null)
            {
                Log.Error("Nie udało się utworzyć produktu o nazwie {0} i cenie {1}.", newProduct.ProductName, newProduct.Price);
                throw new Exception("Nie udało się utworzyć produktu.");
            }

            _dbcontext.Products.Add(entity);
            _dbcontext.SaveChanges();

            Log.Error("Utworzono produkt o nazwie {0} i cenie {1}.", newProduct.ProductName, newProduct.Price);

            return entity.ProductId;
        }
    }
}
