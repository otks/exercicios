using MediatR;
using Questao5.Domain.Enumerators;
using Questao5.Infrastructure.Database.CommandStore.Responses;

namespace Questao5.Infrastructure.Database.CommandStore.Requests;

public class AddAccountMovementQueryRequest : IRequest<AddAccountMovementQueryResponse>
{
    public Guid AccountId { get; private set; }
    public DateTime MovementDate { get; private set; }
    public EAccountMovementType MovementType { get; private set; }
    public decimal Value { get; private set; }

    public AddAccountMovementQueryRequest(Guid accountId, DateTime movementDate, EAccountMovementType movementType,
        decimal value)
    {
        AccountId = accountId;
        MovementDate = movementDate;
        MovementType = movementType;
        Value = value;
    }
}