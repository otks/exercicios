using Questao5.Domain.Entities;

namespace Questao5.Infrastructure.Database.QueryStore.Responses;

public class GetAccountBalanceQueryResponse
{
    public SaldoConta AccountBalance { get; private set; }

    public GetAccountBalanceQueryResponse(SaldoConta accountBalance)
    {
        AccountBalance = accountBalance;
    }
}