using Ambev.DeveloperEvaluation.Application.Users.GetUser;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Events;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Domain.Services;
using AutoMapper;
using FluentValidation;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.CreateSale
{
    public class CreateSaleHandler : IRequestHandler<CreateSaleCommand, CreateSaleResult>
    {
        private readonly ISaleRepository _saleRepository;
        private readonly IMapper _mapper;
        private readonly IBranchService _branchService;
        private readonly IProductService _productService;
        private readonly IMediator _mediator;
        private readonly IDomainEventDispatcher _eventDispatcher;

        public CreateSaleHandler(
            IMapper mapper, 
            ISaleRepository saleRepository, 
            IBranchService branchService,
            IProductService productService,
            IMediator mediator,
            IDomainEventDispatcher eventDispatcher)
        {
            _mapper = mapper;
            _saleRepository = saleRepository;
            _branchService = branchService;
            _productService = productService;
            _mediator = mediator;
            _eventDispatcher = eventDispatcher;
        }

        public async Task<CreateSaleResult> Handle(CreateSaleCommand command, CancellationToken cancellationToken)
        {
            var validator = new CreateSaleValidator();
            var validationResult = await validator.ValidateAsync(command, cancellationToken);

            if (!validationResult.IsValid)
                throw new ValidationException(validationResult.Errors);

            var branch = await _branchService.GetBranchByIdAsync(command.BranchId, cancellationToken);
            if(branch == null)
                throw new InvalidOperationException($"Branch with id {command.BranchId} not found");

            // Levando em consideração que Users estária nesse mesmo microsserviço e assumindo que Customers é um USER com a role Customer
            var getUserCommand = new GetUserCommand(command.CustomerId);
            var user = await _mediator.Send(getUserCommand, cancellationToken);
            if (user == null)
                throw new InvalidOperationException($"Customer with id {command.CustomerId} not found");

            if (user.Role != Domain.Enums.UserRole.Customer)
                throw new InvalidOperationException($"Customer with id {command.CustomerId} not is valid");

            var sale = _mapper.Map<Sale>(command);
            sale.Branch = branch;
            sale.Customer = new()
            {
                Id = user.Id,
                Name = user.Email
            };

            // Exemplo de como você pode usar o Task.WhenAll para processar as consultas em paralelo.
            var tasks = sale.Items.Select(async item =>
            {
                var isDuplicatedProduct = sale.Items.Any(i => i.Product.Id == item.Product.Id && i != item);
                if (isDuplicatedProduct)
                    throw new InvalidOperationException($"Product with id {item.Product.Id} is duplicated in the sale");

                var product = await _productService.GetProductByIdAsync(item.Product.Id, cancellationToken);
                if (product == null)
                    throw new InvalidOperationException($"Product with id {item.Product.Id} not found");

                item.Product = product;
                item.CalculateDiscountPercentage();
            }).ToList();

            await Task.WhenAll(tasks);

            var createdSale = await _saleRepository.CreateAsync(sale, cancellationToken);
            var @event = new SaleCreatedEvent(sale);
            await _eventDispatcher.DispatchAsync(@event);
            var result = _mapper.Map<CreateSaleResult>(createdSale);
            return result;
        }
    }
}
