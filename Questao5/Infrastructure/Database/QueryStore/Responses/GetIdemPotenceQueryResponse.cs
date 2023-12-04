using Questao5.Domain.Entities;

namespace Questao5.Infrastructure.Database.QueryStore.Responses;

public class GetIdemPotenceQueryResponse
{
    public IdemPotencia? IdemPotence { get; private set; }

    public GetIdemPotenceQueryResponse(IdemPotencia? idemPotence)
    {
        IdemPotence = idemPotence;
    }
}