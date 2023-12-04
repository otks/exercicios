using MediatR;
using Questao5.Application.Commands.Requests.Account.AccountMovement;
using Questao5.Application.Commands.Responses.Account.AccountMovement.AddAccountMovement;
using Questao5.Application.Handlers;
using Questao5.Infrastructure.Database.CommandStore.Handlers;
using Questao5.Infrastructure.Database.CommandStore.Requests;
using Questao5.Infrastructure.Database.CommandStore.Responses;

namespace Questao5.Infrastructure.Configuration.IoC;

public static class AccountMovementDependencyInjection
{
    public static IServiceCollection AddAccountMovementDependencyInjection(this IServiceCollection services)
    {
        services
            .AddScoped<IRequestHandler<AddAccountMovementQueryRequest, AddAccountMovementQueryResponse>,
                AddAccountMovementQueryHandler>();

        services
            .AddScoped<IRequestHandler<AddAccountMovementCommand, AddAccountMovementCommandResponse>,
                AddAccountMovementHandler>();
        

        return services;
    }
}