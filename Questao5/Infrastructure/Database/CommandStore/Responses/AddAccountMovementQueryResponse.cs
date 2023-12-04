namespace Questao5.Infrastructure.Database.CommandStore.Responses;

public class AddAccountMovementQueryResponse
{
    public Guid Id { get; private set; }

    public AddAccountMovementQueryResponse(Guid id)
    {
        Id = id;
    }
}