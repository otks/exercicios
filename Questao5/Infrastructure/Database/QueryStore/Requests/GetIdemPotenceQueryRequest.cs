using MediatR;
using Questao5.Infrastructure.Database.QueryStore.Responses;

namespace Questao5.Infrastructure.Database.QueryStore.Requests;

public class GetIdemPotenceQueryRequest: IRequest<GetIdemPotenceQueryResponse>
{
    public Guid RequestId { get; set; }

    public GetIdemPotenceQueryRequest(Guid requestId)
    {
        RequestId = requestId;
    }
}