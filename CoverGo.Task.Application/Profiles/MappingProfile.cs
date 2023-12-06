using AutoMapper;
using CoverGo.Task.Application.Contracts.Services;
using CoverGo.Task.Application.DTO.Discounts;
using CoverGo.Task.Application.DTO.Product;
using CoverGo.Task.Application.DTO.ProductAmount;
using CoverGo.Task.Application.DTO.ShoppingCart;
using CoverGo.Task.Application.Services;
using CoverGo.Task.Domain;

namespace CoverGo.Task.Application.Profiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            #region Repositories

            // Discount
            CreateMap<Discount, CreateDiscountDto>().ReverseMap();


            //////// Product /////////////
            CreateMap<Product, ProductDto>().ReverseMap();
            CreateMap<Product, CreateProductDto>().ReverseMap();

            // Product Amount
            CreateMap<ProductAmount, ProductAmountDto>().ReverseMap();
            CreateMap<ProductAmount, ProductAmountDetailDto>().ReverseMap();
            CreateMap<ProductAmount, AddShoppingCartItemDto>().ReverseMap();

            /// Shopping Cart
            CreateMap<ShoppingCart, ShoppingCartDetailsDto>().ReverseMap();

            #endregion
        }
    }
}
