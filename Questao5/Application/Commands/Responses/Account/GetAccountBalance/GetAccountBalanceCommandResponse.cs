using Questao5.Domain.Enumerators;

namespace Questao5.Application.Commands.Responses.Account.GetAccountBalance;

public class GetAccountBalanceCommandResponse
{
    public string? Message { get; private set; }
    public string? ErrorType { get; private set; }
    public int StatusCode { get; private set; }
    public GetAccountBalanceResponseData? Data { get; private set; }

    public GetAccountBalanceCommandResponse(int statusCode, string? message, EAccountErrors? errorType,
        GetAccountBalanceResponseData? data)
    {
        Message = message;
        ErrorType = MapError(errorType);
        Data = data;
        StatusCode = statusCode;
    }

    private string? MapError(EAccountErrors? errorType)
    {
        if (errorType == null)
            return null;

        switch (errorType)
        {
            case EAccountErrors.None: return null;
            case EAccountErrors.InvalidAccount: return "INVALID_ACCOUNT";
            case EAccountErrors.InactiveAccount: return "INACTIVE_ACCOUNT";
            default:
                throw new ArgumentOutOfRangeException(nameof(errorType), errorType, null);
        }
    }
}