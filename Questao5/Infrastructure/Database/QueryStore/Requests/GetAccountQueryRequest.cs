using MediatR;
using Questao5.Infrastructure.Database.QueryStore.Responses;

namespace Questao5.Infrastructure.Database.QueryStore.Requests;

public class GetAccountQueryRequest : IRequest<GetAccountQueryResponse>
{
    public Guid AccountId { get; private set; }

    public GetAccountQueryRequest(Guid accountId)
    {
        AccountId = accountId;
    }
}