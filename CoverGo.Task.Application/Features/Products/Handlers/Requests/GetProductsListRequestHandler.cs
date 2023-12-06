using AutoMapper;
using CoverGo.Task.Application.Contracts.Persistence;
using CoverGo.Task.Application.DTO.Product;
using CoverGo.Task.Application.Features.Products.Requests.Queries;
using MediatR;

namespace CoverGo.Task.Application.Features.Products.Handlers.Requests
{
    public class GetProductsListRequestHandler : IRequestHandler<GetProductsListRequest, IReadOnlyList<ProductDto>>
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;
        public GetProductsListRequestHandler(IProductRepository productRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }
        public async Task<IReadOnlyList<ProductDto>> Handle(GetProductsListRequest request, CancellationToken cancellationToken)
        {
            var products =  await _productRepository.GetAll();
            return _mapper.Map<IReadOnlyList<ProductDto>>(products);
        }
    }
}