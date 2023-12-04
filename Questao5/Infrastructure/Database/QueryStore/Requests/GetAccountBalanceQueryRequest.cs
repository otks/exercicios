using MediatR;
using Questao5.Infrastructure.Database.QueryStore.Responses;

namespace Questao5.Infrastructure.Database.QueryStore.Requests;

public class GetAccountBalanceQueryRequest : IRequest<GetAccountBalanceQueryResponse>
{
    public Guid AccountId { get; private set; }

    public GetAccountBalanceQueryRequest(Guid accountId)
    {
        AccountId = accountId;
    }
}