using MediatR;
using Questao5.Infrastructure.Database.CommandStore.Handlers;
using Questao5.Infrastructure.Database.CommandStore.Requests;
using Questao5.Infrastructure.Database.CommandStore.Responses;
using Questao5.Infrastructure.Database.QueryStore.Handlers;
using Questao5.Infrastructure.Database.QueryStore.Requests;
using Questao5.Infrastructure.Database.QueryStore.Responses;

namespace Questao5.Infrastructure.Configuration.IoC;

public static class IdemPotenceDependencyInjection
{
    public static IServiceCollection AddIdemPotenceDependencyInjection(this IServiceCollection services)
    {
        services
            .AddScoped<IRequestHandler<AddIdempotenceQueryRequest, AddIdempotenceQueryResponce>,
                AddIdempotenceQueryHandler>();

        services
            .AddScoped<IRequestHandler<GetIdemPotenceQueryRequest, GetIdemPotenceQueryResponse>,
                GetIdemPotenceQueryHandler>();

        return services;
    }
}