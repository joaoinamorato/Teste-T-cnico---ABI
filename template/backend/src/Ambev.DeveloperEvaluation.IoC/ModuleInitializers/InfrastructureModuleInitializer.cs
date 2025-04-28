using Ambev.DeveloperEvaluation.Domain.Events;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Domain.Services;
using Ambev.DeveloperEvaluation.Grpc.Protos;
using Ambev.DeveloperEvaluation.GrpcClients.Branch;
using Ambev.DeveloperEvaluation.GrpcClients.Product;
using Ambev.DeveloperEvaluation.MessageBroker;
using Ambev.DeveloperEvaluation.ORM;
using Ambev.DeveloperEvaluation.ORM.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Rebus.Config;
using Rebus.Transport.InMem;

namespace Ambev.DeveloperEvaluation.IoC.ModuleInitializers;

public class InfrastructureModuleInitializer : IModuleInitializer
{
    public void Initialize(WebApplicationBuilder builder)
    {
        #region ORM
        builder.Services.AddScoped<DbContext>(provider => provider.GetRequiredService<DefaultContext>());
        builder.Services.AddScoped<IUserRepository, UserRepository>();
        builder.Services.AddScoped<ISaleRepository, SaleRepository>();
        #endregion

        #region Grpc
        builder.Services.AddGrpcClient<BranchGrpc.BranchGrpcClient>(options =>
        {
            options.Address = new Uri("https://api-branch-service:443");
        });

        // MOCK para simular o serviço
        //builder.Services.AddScoped<IBranchService, BranchServiceGrpc>();
        builder.Services.AddScoped<IBranchService, MockBranchService>();

        builder.Services.AddGrpcClient<ProductGrpc.ProductGrpcClient>(options =>
        {
            options.Address = new Uri("https://api-product-service:443");
        });

        // MOCK para simular o serviço
        //builder.Services.AddScoped<IProductService, ProductServiceGrpc>();
        builder.Services.AddScoped<IProductService, MockProductService>();

        #endregion

        #region Message Broker

        //InMemory para exemplo, mas pode ser substituido por algum Message Broker
        builder.Services.AddRebus(config => config
            .Transport(t => t.UseInMemoryTransport(new InMemNetwork(), "developer-store-events"))
        );

        builder.Services.AddScoped<IDomainEventDispatcher, RebusDomainEventDispatcher>();

        //Desconsiderei a configuração de consumo (handlers)
        #endregion
    }
}