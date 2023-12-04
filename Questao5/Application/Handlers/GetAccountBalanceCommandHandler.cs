using MediatR;
using Questao5.Application.Commands.Requests.Account;
using Questao5.Application.Commands.Responses.Account.GetAccountBalance;
using Questao5.Domain.Enumerators;
using Questao5.Infrastructure.Database.QueryStore.Requests;
using Questao5.Infrastructure.Database.QueryStore.Responses;

namespace Questao5.Application.Handlers;

public class GetAccountBalanceCommandHandler :
    IRequestHandler<GetAccountBalanceCommandRequest, GetAccountBalanceCommandResponse>
{
    private readonly IMediator _mediator;

    public GetAccountBalanceCommandHandler(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task<GetAccountBalanceCommandResponse> Handle(GetAccountBalanceCommandRequest request,
        CancellationToken cancellationToken)
    {
        GetAccountQueryRequest getAccountCommand = new(request.AccountId);
        GetAccountQueryResponse account = await _mediator.Send(getAccountCommand, cancellationToken);

        if (account.Account == null)
        {
            return new GetAccountBalanceCommandResponse(400,
                "Apenas contas correntes cadastradas podem receber movimentação.",
                EAccountErrors.InvalidAccount, null);
        }

        if (!account.Account!.Ativo)
        {
            return new GetAccountBalanceCommandResponse(400,
                "Apenas contas correntes ativas podem receber movimentação.",
                EAccountErrors.InactiveAccount, null);
        }

        GetAccountBalanceQueryRequest getBalanceRequest = new(request.AccountId);
        GetAccountBalanceQueryResponse accountBalance = await _mediator.Send(getBalanceRequest, cancellationToken);

        return new GetAccountBalanceCommandResponse(200, "Saldo consultado com sucesso.", null,
            new GetAccountBalanceResponseData(
                accountBalance.AccountBalance.Nome, 
                accountBalance.AccountBalance.Numero, 
                accountBalance.AccountBalance.Saldo,
                accountBalance.AccountBalance.DataHoraConsulta));
    }
}