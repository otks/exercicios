using Questao5.Domain.Enumerators;

namespace Questao5.Application.Commands.Responses.Account.AccountMovement.AddAccountMovement;

public class AddAccountMovementCommandResponse
{
    public string? Message { get; private set; }
    public string? ErrorType { get; private set; }
    public int StatusCode { get; private set; }
    public AddAccountMovementResponseData? Data { get; private set; }

    public AddAccountMovementCommandResponse(int statusCode, string? message, EAccountMovementErrors? errorType, 
        AddAccountMovementResponseData? data)
    {
        Message = message;
        ErrorType = MapError(errorType);
        Data = data;
        StatusCode = statusCode;
    }

    private string? MapError(EAccountMovementErrors? errorType)
    {
        if (errorType == null)
            return null;

        switch (errorType)
        {
            case EAccountMovementErrors.None: return null;
            case EAccountMovementErrors.InvalidAccount: return "INVALID_ACCOUNT";
            case EAccountMovementErrors.InactiveAccount: return "INACTIVE_ACCOUNT";
            case EAccountMovementErrors.InvalidValue: return "INVALID_VALUE";
            case EAccountMovementErrors.InvalidType: return "INVALID_TYPE";
            default:
                throw new ArgumentOutOfRangeException(nameof(errorType), errorType, null);
        }
    }
}