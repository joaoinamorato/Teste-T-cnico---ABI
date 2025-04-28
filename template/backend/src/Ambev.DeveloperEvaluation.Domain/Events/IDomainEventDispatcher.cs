namespace Ambev.DeveloperEvaluation.Domain.Events
{
    public interface IDomainEventDispatcher
    {
        Task DispatchAsync<T>(T domainEvent) where T : class;
    }
}
