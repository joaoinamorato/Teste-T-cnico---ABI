using Ambev.DeveloperEvaluation.Domain.Events;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using FluentValidation;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.CancelSale
{
    /// <summary>
    /// Handler for processing CancelSaleHandler requests
    /// </summary>
    public class CancelSaleHandler : IRequestHandler<CancelSaleCommand, CancelSaleResponse>
    {
        private readonly ISaleRepository _saleRepository;
        private readonly IDomainEventDispatcher _eventDispatcher;

        /// <summary>
        /// Initializes a new instance of CancelSaleHandler
        /// </summary>
        /// <param name="saleRepository">The sale repository</param>
        /// <param name="eventDispatcher">The event dispatcher</param>
        public CancelSaleHandler(
            ISaleRepository saleRepository,
            IDomainEventDispatcher eventDispatcher)
        {
            _saleRepository = saleRepository;
            _eventDispatcher = eventDispatcher;
        }

        /// <summary>
        /// Handles the CancelSaleHandler request
        /// </summary>
        /// <param name="request">The CancelSale command</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>The result of the cancel operation</returns>
        public async Task<CancelSaleResponse> Handle(CancelSaleCommand request, CancellationToken cancellationToken)
        {
            var validator = new CancelSaleValidator();
            var validationResult = await validator.ValidateAsync(request, cancellationToken);

            if (!validationResult.IsValid)
                throw new ValidationException(validationResult.Errors);

            var sale = await _saleRepository.GetByIdAsync(request.Id, cancellationToken);
            if (sale is null)
                throw new KeyNotFoundException($"User with ID {request.Id} not found");

            sale.Cancel();
            await _saleRepository.UpdateAsync(sale, cancellationToken);
            var @event = new SaleCanceledEvent(sale.Id);
            await _eventDispatcher.DispatchAsync(@event);
            return new CancelSaleResponse { Success = true };
        }
    }
}
