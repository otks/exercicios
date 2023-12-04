namespace Questao5.Application.Commands.Responses.Account.GetAccountBalance;

public class GetAccountBalanceResponseData
{
    public decimal Balance { get; private set; }
    public DateTime ConsultDateTime { get; private set; }
    public int AccountNumber { get; private set; }
    public string Name { get; private set; }
    
    public GetAccountBalanceResponseData(string name, int accountNumber, decimal balance, DateTime dateTime)
    {
        Name = name;
        AccountNumber = accountNumber;
        Balance = balance;
        ConsultDateTime = dateTime;
    }
}