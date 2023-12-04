namespace Questao5.Application.Commands.Responses.Account.AccountMovement.AddAccountMovement;

public class AddAccountMovementResponseData
{
    public Guid Id { get; private set; }

    public AddAccountMovementResponseData(Guid id)
    {
        Id = id;
    }
}