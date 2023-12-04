using MediatR;
using Newtonsoft.Json;
using Questao5.Application.Commands.Requests.Account.AccountMovement;
using Questao5.Application.Commands.Responses.Account.AccountMovement.AddAccountMovement;
using Questao5.Domain.Enumerators;
using Questao5.Infrastructure.Database.CommandStore.Requests;
using Questao5.Infrastructure.Database.CommandStore.Responses;
using Questao5.Infrastructure.Database.QueryStore.Requests;
using Questao5.Infrastructure.Database.QueryStore.Responses;

namespace Questao5.Application.Handlers;

public class AddAccountMovementHandler :
    IRequestHandler<AddAccountMovementCommand, AddAccountMovementCommandResponse>
{
    private readonly IMediator _mediator;

    public AddAccountMovementHandler(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task<AddAccountMovementCommandResponse> Handle(AddAccountMovementCommand request, CancellationToken cancellationToken)
    {
        GetAccountQueryRequest getAccountCommand = new(request.AccountId);
        GetAccountQueryResponse account = await _mediator.Send(getAccountCommand, cancellationToken);

        if (account.Account == null)
        {
            return new AddAccountMovementCommandResponse(400,
                "Apenas contas correntes cadastradas podem receber movimentação.",
                EAccountMovementErrors.InvalidAccount, null);
        }

        if (!account.Account!.Ativo)
        {
            return new AddAccountMovementCommandResponse(400,
                "Apenas contas correntes ativas podem receber movimentação.",
                EAccountMovementErrors.InactiveAccount, null);
        }

        if (request.MovementValue < 0)
        {
            return new AddAccountMovementCommandResponse(400,
                "Apenas valores positivos podem ser recebidos.",
                EAccountMovementErrors.InvalidValue, null);
        }
        
        bool enumConverted = Enum.TryParse(request.MovementType, true, out EAccountMovementType accountType);
        if (!enumConverted || accountType == EAccountMovementType.None)
        {
            return new AddAccountMovementCommandResponse(400,
                "Apenas os tipos “débito” ou “crédito” podem ser aceitos.",
                EAccountMovementErrors.InvalidType, null);
        }
        
        GetIdemPotenceQueryRequest getIdemPotenceRequest = new(request.RequestId);
        GetIdemPotenceQueryResponse requestExist = await _mediator.Send(getIdemPotenceRequest);
        if (requestExist.IdemPotence != null)
        {
            AddAccountMovementCommandResponse idemPotenceResponse = 
                JsonConvert.DeserializeObject<AddAccountMovementCommandResponse>(requestExist.IdemPotence.Resultado)!;
            
            return idemPotenceResponse;
        }
        
        AddAccountMovementQueryRequest accountMovementQuery = new(
            request.AccountId, DateTime.Now, accountType, request.MovementValue);

        AddAccountMovementQueryResponse accountMovementQueryResponse = 
            await _mediator.Send(accountMovementQuery, cancellationToken);

        AddAccountMovementCommandResponse response = new (200, "Movimentado a conta com sucesso.", null, 
            new AddAccountMovementResponseData(accountMovementQueryResponse.Id));

        string requestSerialized = JsonConvert.SerializeObject(request);
        string responseSerialized = JsonConvert.SerializeObject(response);
        AddIdempotenceQueryRequest saveIdemPotenceRequest = new (request.RequestId, requestSerialized, responseSerialized);
        await _mediator.Send(saveIdemPotenceRequest, cancellationToken);
        
        return response;
    }
}