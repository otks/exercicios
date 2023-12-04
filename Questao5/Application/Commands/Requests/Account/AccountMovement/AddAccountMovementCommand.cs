using MediatR;
using Questao5.Application.Commands.Responses.Account.AccountMovement.AddAccountMovement;

namespace Questao5.Application.Commands.Requests.Account.AccountMovement;

public class AddAccountMovementCommand: IRequest<AddAccountMovementCommandResponse>
{
    public Guid RequestId { get; private set; }
    public Guid AccountId { get; private set; }
    public decimal MovementValue { get; private set; }
    public string MovementType { get; private set; }

    public AddAccountMovementCommand(Guid requestId, Guid accountId, decimal movementValue, string movementType)
    {
        RequestId = requestId;
        AccountId = accountId;
        MovementValue = movementValue;
        MovementType = movementType;
    }
}