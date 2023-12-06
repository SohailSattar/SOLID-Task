using AutoMapper;
using CoverGo.Task.Application.Contracts.Persistence;
using CoverGo.Task.Application.DTO.Product.Validators;
using CoverGo.Task.Application.Features.Products.Requests.Commands;
using CoverGo.Task.Application.Responses;
using CoverGo.Task.Domain;
using MediatR;

namespace CoverGo.Task.Application.Features.Products.Handlers.Commands
{
    public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, BaseCommandResponse>
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;
        public CreateProductCommandHandler(IProductRepository productRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }
        public async Task<BaseCommandResponse> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();
            var validator = new CreateProductDtoValidator(_productRepository);

            // Validate mandatory fields
            var validationResult = await validator.ValidateAsync(request.ProductDto!);

            if (!validationResult.IsValid)
            {
                response.Success = false;
                response.Message = "Creation failed";
                response.Errors = validationResult.Errors.Select(q => q.ErrorMessage).ToList();

                return response;
            }

            var product = _mapper.Map<Product>(request.ProductDto);
            product = await _productRepository.Add(product);

            response.Success = true;
            response.Message = "Creation Successful";
            response.Id = product.Id;


            return response;
        }
    }
}