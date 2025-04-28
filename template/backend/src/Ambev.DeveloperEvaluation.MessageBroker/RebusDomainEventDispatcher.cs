using Ambev.DeveloperEvaluation.Domain.Events;
using Rebus.Bus;

namespace Ambev.DeveloperEvaluation.MessageBroker
{
    public class RebusDomainEventDispatcher : IDomainEventDispatcher
    {
        private readonly IBus _bus;

        public RebusDomainEventDispatcher(IBus bus)
        {
            _bus = bus;
        }

        public async Task DispatchAsync<T>(T domainEvent) where T : class
        {
            await _bus.Publish(domainEvent);
        }
    }
}
