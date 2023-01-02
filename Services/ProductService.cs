using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Serilog;
using ShopTI.Entities;
using ShopTI.Enums;
using ShopTI.IServices;
using ShopTI.Models;

namespace ShopTI.Services
{
    public class ProductService : IProductService
    {
        private readonly ShopDbContext _dbcontext;
        private readonly IMapper _mapper;
        private readonly IUserContextService _userContextService;
        private readonly IAuthorizationService _authorizationService;
        public ProductService(
            ShopDbContext dbcontext, 
            IMapper mapper,
            IUserContextService userContextService,
            IAuthorizationService authorizationService)
        {
            _dbcontext = dbcontext;
            _mapper = mapper;
            _userContextService = userContextService;
            _authorizationService = authorizationService;
        }

        public int CreateProduct(CreateProductModel newProduct)
        {
            var entity = _mapper.Map<Product>(newProduct);
            if (entity == null)
            {
                Log.Error("Nie udało się utworzyć produktu o nazwie {0} i cenie {1}.", newProduct.ProductName, newProduct.Price);
                throw new Exception("Nie udało się utworzyć produktu.");
            }
          
            var authorizationResult = _authorizationService
                .AuthorizeAsync(_userContextService.User, entity, new ResourceOperationRequirement(ResourceOperation.Create))
                .Result;
           
            if (!authorizationResult.Succeeded)
            {
                Log.Error("Błąd przy autoryzacji użytkownika o id {0}.", _userContextService.GetUserId);
                throw new Exception("Błąd przy autoryzacji użytkownika.");
            } 

            _dbcontext.Products.Add(entity);
            _dbcontext.SaveChanges();

            Log.Information("Utworzono produkt o nazwie {0} i cenie {1} przez użytkownika.", 
                newProduct.ProductName, newProduct.Price);

            return entity.ProductId;
        }
    }
}
