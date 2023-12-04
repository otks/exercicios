using Questao5.Domain.Entities;

namespace Questao5.Infrastructure.Database.QueryStore.Responses;

public class GetAccountQueryResponse
{
    public ContaCorrente? Account { get; private set; }
    
    public GetAccountQueryResponse(ContaCorrente account)
    {
        Account = account;
    }
}