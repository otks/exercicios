using MediatR;
using NSubstitute;
using Questao5.Application.Commands.Requests.Account;
using Questao5.Application.Commands.Responses.Account.GetAccountBalance;
using Questao5.Application.Handlers;
using Questao5.Domain.Entities;
using Questao5.Infrastructure.Database.QueryStore.Requests;
using Questao5.Infrastructure.Database.QueryStore.Responses;
using Xunit;

namespace Questao5.UnitTests.Application.Handlers;

[Trait("GetAccountBalanceCommandHandler", "Unit Tests")]
public class GetAccountBalanceCommandHandlerTests
{
    [Fact]
    public async void AccountBalance_InvalidAccount()
    {
        // Arrange
        GetAccountBalanceCommandRequest getAccountBalanceCommandRequest =
            new(Guid.Parse("aaf6f634-3e23-4738-8d92-b0517b36e0a0"));

        IMediator? mediatr = Substitute.For<IMediator>();
        mediatr.Send(Arg.Any<GetAccountQueryRequest>()).Returns(new GetAccountQueryResponse(null));

        GetAccountBalanceCommandHandler getAccountBalance = new(mediatr);

        // Act
        GetAccountBalanceCommandResponse result =
            await getAccountBalance.Handle(getAccountBalanceCommandRequest, new CancellationToken());

        // Assert
        Assert.Equal("Apenas contas correntes cadastradas podem receber movimentação.", result.Message);
        Assert.Equal(400, result.StatusCode);
        Assert.Equal("INVALID_ACCOUNT", result.ErrorType);
    }

    [Fact]
    public async void AccountBalance_InactiveAccount()
    {
        // Arrange
        GetAccountBalanceCommandRequest getAccountBalanceCommandRequest =
            new(Guid.Parse("aaf6f634-3e23-4738-8d92-b0517b36e0a0"));

        IMediator? mediatr = Substitute.For<IMediator>();
        mediatr.Send(Arg.Any<GetAccountQueryRequest>()).Returns(new GetAccountQueryResponse(
            new ContaCorrente("123", 234, "Teste", false)));

        GetAccountBalanceCommandHandler getAccountBalance = new(mediatr);

        // Act
        GetAccountBalanceCommandResponse result =
            await getAccountBalance.Handle(getAccountBalanceCommandRequest, new CancellationToken());

        // Assert
        Assert.Equal("Apenas contas correntes ativas podem receber movimentação.", result.Message);
        Assert.Equal(400, result.StatusCode);
        Assert.Equal("INACTIVE_ACCOUNT", result.ErrorType);
    }

    [Fact]
    public async void AccountBalance_Success()
    {
        // Arrange
        GetAccountBalanceCommandRequest getAccountBalanceCommandRequest =
            new(Guid.Parse("aaf6f634-3e23-4738-8d92-b0517b36e0a0"));
        GetAccountQueryResponse getAccountQueryResponse = new(new ContaCorrente("123", 234, "Teste", true));
        
        DateTime dataHoraConsulta = DateTime.Now;
        GetAccountBalanceQueryResponse getAccountBalanceQueryResponse =
            new(new SaldoConta(1234, "Teste", 333, dataHoraConsulta));

        IMediator? mediatr = Substitute.For<IMediator>();
        mediatr.Send(Arg.Any<GetAccountQueryRequest>()).Returns(getAccountQueryResponse);
        mediatr.Send(Arg.Any<GetAccountBalanceQueryRequest>()).Returns(getAccountBalanceQueryResponse);

        GetAccountBalanceCommandHandler getAccountBalance = new(mediatr);

        // Act
        GetAccountBalanceCommandResponse result =
            await getAccountBalance.Handle(getAccountBalanceCommandRequest, new CancellationToken());

        // Assert
        Assert.Equal("Saldo consultado com sucesso.", result.Message);
        Assert.Equal(200, result.StatusCode);
        Assert.Equal(1234, result.Data!.AccountNumber);
        Assert.Equal("Teste", result.Data.Name);
        Assert.Equal(333, result.Data.Balance);
        Assert.Equal(dataHoraConsulta, result.Data.ConsultDateTime);
    }
}