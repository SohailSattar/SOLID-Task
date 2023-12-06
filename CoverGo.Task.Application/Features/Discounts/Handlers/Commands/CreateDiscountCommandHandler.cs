using AutoMapper;
using CoverGo.Task.Application.Contracts.Persistence;
using CoverGo.Task.Application.DTO.Discounts.Validators;
using CoverGo.Task.Application.DTO.Product.Validators;
using CoverGo.Task.Application.Features.Discounts.Queries.Commands;
using CoverGo.Task.Application.Responses;
using CoverGo.Task.Domain;
using MediatR;

namespace CoverGo.Task.Application.Features.Discounts.Handlers.Commands
{
    public class CreateDiscountCommandHandler : IRequestHandler<CreateDiscountCommand, BaseCommandResponse>
    {
        private readonly IDiscountRepository _discountRepository;
        private readonly IMapper _mapper;
        public CreateDiscountCommandHandler(IDiscountRepository discountRepository, IMapper mapper)
        {
            _discountRepository = discountRepository;
            _mapper = mapper;
        }

        public async Task<BaseCommandResponse> Handle(CreateDiscountCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();
            var validator = new CreateDiscountDtoValidator(_discountRepository);

            // Validate mandatory fields
            var validationResult = await validator.ValidateAsync(request.DiscountDto!);

            if (!validationResult.IsValid)
            {
                response.Success = false;
                response.Message = "Creation failed";
                response.Errors = validationResult.Errors.Select(q => q.ErrorMessage).ToList();

                return response;
            }

            if(request.DiscountDto == null)
            {
                response.Success = false;
                response.Message = "Creation failed";
                response.Errors = new List<string> { "Empty Discount details" };

                return response;
            }

            // If Product Exists
            var isExist = await _discountRepository.CheckIfProductExists(request.DiscountDto.ProductId!);
            if(!isExist)
            {
                response.Success = false;
                response.Message = "Creation failed";
                response.Errors = new List<string> { "Product doesnt exist" };

                return response;

            }


            var discount = _mapper.Map<Discount>(request.DiscountDto);
            discount = await _discountRepository.Add(discount);

            response.Success = true;
            response.Message = "Creation Successful";
            response.Id = discount.Id;


            return response;
        }
    }
}
