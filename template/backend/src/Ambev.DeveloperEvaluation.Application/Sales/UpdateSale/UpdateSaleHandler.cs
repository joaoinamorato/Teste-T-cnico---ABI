using Ambev.DeveloperEvaluation.Application.Users.GetUser;
using Ambev.DeveloperEvaluation.Domain.Events;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Domain.Services;
using AutoMapper;
using FluentValidation;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.UpdateSale
{
    /// <summary>
    /// Handler for processing UpdateSaleCommand requests
    /// </summary>
    public class UpdateSaleHandler : IRequestHandler<UpdateSaleCommand, UpdateSaleResult>
    {
        private readonly ISaleRepository _saleRepository;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;
        private readonly IBranchService _branchService;
        private readonly IDomainEventDispatcher _eventDispatcher;

        /// <summary>
        /// Initializes a new instance of UpdateSaleHandler
        /// </summary>
        /// <param name="saleRepository">The sale repository</param>
        /// <param name="mapper">The AutoMapper instance</param>
        /// <param name="eventDispatcher">The event dispatcher</param>
        /// <param name="mediator">The mediator</param>
        /// <param name="branchService">The branch service</param>
        public UpdateSaleHandler(
            ISaleRepository saleRepository,
            IMapper mapper,
            IMediator mediator,
            IBranchService branchService,
            IDomainEventDispatcher eventDispatcher)
        {
            _saleRepository = saleRepository;
            _mapper = mapper;
            _mediator = mediator;
            _branchService = branchService;
            _eventDispatcher = eventDispatcher;
        }

        /// <summary>
        /// Handles the UpdateSaleCommand request
        /// </summary>
        /// <param name="request">The UpdateSale command</param>
        /// <param name="cancellationToken">Cancellation token</param>  
        /// <returns>The sale updated</returns>
        public async Task<UpdateSaleResult> Handle(UpdateSaleCommand request, CancellationToken cancellationToken)
        {
            // Para o UPDATE desconsiderei o update dos itens, levando em consideração um UseCase especifico para o SaleItem
            var validator = new UpdateSaleValidator();
            var validationResult = await validator.ValidateAsync(request, cancellationToken);

            if (!validationResult.IsValid)
                throw new ValidationException(validationResult.Errors);

            var sale = await _saleRepository.GetByIdAsync(request.Id, cancellationToken);
            if (sale == null)
                throw new KeyNotFoundException($"Sale with ID {request.Id} not found");

            if (sale.Branch.Id != request.BranchId)
            {
                var branch = await _branchService.GetBranchByIdAsync(request.BranchId, cancellationToken);
                if (branch == null)
                    throw new InvalidOperationException($"Branch with id {request.BranchId} not found");

                sale.Branch = branch;
            }

            // Levando em consideração que Users estária nesse mesmo microsserviço e assumindo que Customers é um USER com a role Customer
            if (sale.Customer.Id != request.CustomerId)
            {
                var getUserCommand = new GetUserCommand(request.CustomerId);
                var user = await _mediator.Send(getUserCommand, cancellationToken);
                if (user == null)
                    throw new InvalidOperationException($"Customer with id {request.CustomerId} not found");

                if (user.Role != Domain.Enums.UserRole.Customer)
                    throw new InvalidOperationException($"Customer with id {request.CustomerId} not is valid");

                sale.Customer = new()
                {
                    Id = user.Id,
                    Name = user.Email
                };
            }

            sale.Update();
            await _saleRepository.UpdateAsync(sale, cancellationToken);
            var @event = new SaleUpdatedEvent(sale);
            await _eventDispatcher.DispatchAsync(@event);
            return _mapper.Map<UpdateSaleResult>(sale);
        }
    }
}
