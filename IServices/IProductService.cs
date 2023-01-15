using ShopTI.Entities;
using ShopTI.Models;

namespace ShopTI.IServices
{
    public interface IProductService
    {
        int CreateProduct(CreateProductModel newProduct);
        List<Product> GetProducts();

    }
}
