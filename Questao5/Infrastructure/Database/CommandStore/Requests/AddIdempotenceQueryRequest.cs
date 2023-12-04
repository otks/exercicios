using MediatR;
using Questao5.Infrastructure.Database.CommandStore.Responses;

namespace Questao5.Infrastructure.Database.CommandStore.Requests;

public class AddIdempotenceQueryRequest: IRequest<AddIdempotenceQueryResponce>
{
    public Guid RequestId { get; set; }
    public string Request { get; set; }
    public string Response { get; set; }

    public AddIdempotenceQueryRequest(Guid requestId, string request, string response)
    {
        RequestId = requestId;
        Request = request;
        Response = response;
    }
}