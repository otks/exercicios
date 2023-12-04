using MediatR;
using Questao5.Application.Commands.Responses.Account.GetAccountBalance;

namespace Questao5.Application.Commands.Requests.Account;

public class GetAccountBalanceCommandRequest: IRequest<GetAccountBalanceCommandResponse>
{
    public Guid AccountId { get; set; }

    public GetAccountBalanceCommandRequest(Guid accountId)
    {
        AccountId = accountId;
    }
}