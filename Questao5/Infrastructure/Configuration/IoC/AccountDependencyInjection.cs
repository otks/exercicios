using MediatR;
using Questao5.Application.Commands.Requests.Account;
using Questao5.Application.Commands.Responses.Account.GetAccountBalance;
using Questao5.Application.Handlers;
using Questao5.Infrastructure.Database.QueryStore.Handlers;
using Questao5.Infrastructure.Database.QueryStore.Requests;
using Questao5.Infrastructure.Database.QueryStore.Responses;

namespace Questao5.Infrastructure.Configuration.IoC;

public static class AccountDependencyInjection
{
    public static IServiceCollection AddAccountDependencyInjection(this IServiceCollection services)
    {
        services
            .AddScoped<IRequestHandler<GetAccountQueryRequest, GetAccountQueryResponse>,
                GetAccountQueryHandler>();

        services
            .AddScoped<IRequestHandler<GetAccountBalanceCommandRequest, GetAccountBalanceCommandResponse>,
                GetAccountBalanceCommandHandler>();

        return services;
    }
}