using MediatR;
using NSubstitute;
using NSubstitute.ReturnsExtensions;
using Questao5.Application.Commands.Requests.Account.AccountMovement;
using Questao5.Application.Commands.Responses.Account.AccountMovement.AddAccountMovement;
using Questao5.Application.Handlers;
using Questao5.Domain.Entities;
using Questao5.Infrastructure.Database.CommandStore.Requests;
using Questao5.Infrastructure.Database.CommandStore.Responses;
using Questao5.Infrastructure.Database.QueryStore.Requests;
using Questao5.Infrastructure.Database.QueryStore.Responses;
using Xunit;

namespace Questao5.UnitTests.Application.Handlers;

[Trait("AddAccountMovementHandler", "Unit Tests")]
public class AddAccountMovementHandlerTests
{
    [Fact]
    public async void AccountMovement_InvalidAccount()
    {
        // Arrange
        IMediator? mediatr = Substitute.For<IMediator>();
        mediatr.Send(Arg.Any<GetAccountQueryRequest>()).Returns(new GetAccountQueryResponse(null));

        AddAccountMovementHandler addAccountHandler = new(mediatr);

        AddAccountMovementCommand addAccountMovementCommand = new(
            Guid.Parse("89bd9fb4-21c1-4cc3-aa79-9850bbfed7a1"), Guid.Parse("89bd9fb4-21c1-4cc3-aa79-9850bbfed7a2"), 1,
            "C");

        // Act
        AddAccountMovementCommandResponse result =
            await addAccountHandler.Handle(addAccountMovementCommand, new CancellationToken());

        // Assert
        Assert.Equal("Apenas contas correntes cadastradas podem receber movimentação.", result.Message);
        Assert.Equal(400, result.StatusCode);
        Assert.Equal("INVALID_ACCOUNT", result.ErrorType);
    }

    [Fact]
    public async void AccountMovement_InactiveAccount()
    {
        // Arrange
        IMediator? mediatr = Substitute.For<IMediator>();
        mediatr.Send(Arg.Any<GetAccountQueryRequest>()).Returns(new GetAccountQueryResponse(
            new ContaCorrente("123", 234, "Teste", false)));

        AddAccountMovementHandler addAccountHandler = new(mediatr);

        AddAccountMovementCommand addAccountMovementCommand = new(
            Guid.Parse("89bd9fb4-21c1-4cc3-aa79-9850bbfed7a1"), Guid.Parse("89bd9fb4-21c1-4cc3-aa79-9850bbfed7a2"), 1,
            "C");

        // Act
        AddAccountMovementCommandResponse result =
            await addAccountHandler.Handle(addAccountMovementCommand, new CancellationToken());

        // Assert
        Assert.Equal("Apenas contas correntes ativas podem receber movimentação.", result.Message);
        Assert.Equal(400, result.StatusCode);
        Assert.Equal("INACTIVE_ACCOUNT", result.ErrorType);
    }

    [Fact]
    public async void AccountMovement_NegativeMovement()
    {
        // Arrange
        IMediator? mediatr = Substitute.For<IMediator>();
        mediatr.Send(Arg.Any<GetAccountQueryRequest>()).Returns(new GetAccountQueryResponse(
            new ContaCorrente("89bd9fb4-21c1-4cc3-aa79-9850bbfed7a2", 234, "Teste", true)));

        AddAccountMovementHandler addAccountHandler = new(mediatr);

        AddAccountMovementCommand addAccountMovementCommand = new(
            Guid.Parse("89bd9fb4-21c1-4cc3-aa79-9850bbfed7a1"), Guid.Parse("89bd9fb4-21c1-4cc3-aa79-9850bbfed7a2"), -1,
            "C");

        // Act
        AddAccountMovementCommandResponse result =
            await addAccountHandler.Handle(addAccountMovementCommand, new CancellationToken());

        // Assert
        Assert.Equal("Apenas valores positivos podem ser recebidos.", result.Message);
        Assert.Equal(400, result.StatusCode);
        Assert.Equal("INVALID_VALUE", result.ErrorType);
    }

    [Fact]
    public async void AccountMovement_InvalidMovementType()
    {
        // Arrange
        IMediator? mediatr = Substitute.For<IMediator>();
        mediatr.Send(Arg.Any<GetAccountQueryRequest>()).Returns(new GetAccountQueryResponse(
            new ContaCorrente("89bd9fb4-21c1-4cc3-aa79-9850bbfed7a2", 234, "Teste", true)));

        AddAccountMovementHandler addAccountHandler = new(mediatr);

        AddAccountMovementCommand addAccountMovementCommand = new(
            Guid.Parse("89bd9fb4-21c1-4cc3-aa79-9850bbfed7a1"), Guid.Parse("89bd9fb4-21c1-4cc3-aa79-9850bbfed7a2"), 1,
            "G");

        // Act
        AddAccountMovementCommandResponse result =
            await addAccountHandler.Handle(addAccountMovementCommand, new CancellationToken());

        // Assert
        Assert.Equal("Apenas os tipos “débito” ou “crédito” podem ser aceitos.", result.Message);
        Assert.Equal(400, result.StatusCode);
        Assert.Equal("INVALID_TYPE", result.ErrorType);
    }

    [Fact]
    public async void AccountMovement_WithIdemPotence()
    {
        // Arrange
        IMediator? mediatr = Substitute.For<IMediator>();
        mediatr.Send(Arg.Any<GetAccountQueryRequest>()).Returns(new GetAccountQueryResponse(
            new ContaCorrente("89bd9fb4-21c1-4cc3-aa79-9850bbfed7a2", 234, "Teste", true)));

        IdemPotencia idemPotencia = new(
            "3FA85F64-5717-4562-B3FC-2C963F66AFA8",
            "{\n  \"RequestId\": \"3fa85f64-5717-4562-b3fc-2c963f66afa8\",\n  \"AccountId\": \"b6bafc09-6967-ed11-a567-055dfa4a16c9\",\n  \"MovementValue\": 50.0,\n  \"MovementType\": \"D\"\n}",
            "{\n  \"Message\": \"Movimentado a conta com sucesso.\",\n  \"ErrorType\": null,\n  \"StatusCode\": 200,\n  \"Data\": {\n    \"Id\": \"13ff29e6-8508-4637-965c-0f44817c697f\"\n  }\n}");

        mediatr.Send(Arg.Any<GetIdemPotenceQueryRequest>()).Returns(new GetIdemPotenceQueryResponse(idemPotencia));

        AddAccountMovementHandler addAccountHandler = new(mediatr);

        AddAccountMovementCommand addAccountMovementCommand = new(
            Guid.Parse("89bd9fb4-21c1-4cc3-aa79-9850bbfed7a1"), Guid.Parse("89bd9fb4-21c1-4cc3-aa79-9850bbfed7a2"), 1,
            "C");

        // Act
        AddAccountMovementCommandResponse result =
            await addAccountHandler.Handle(addAccountMovementCommand, new CancellationToken());

        // Assert
        Assert.Equal("Movimentado a conta com sucesso.", result.Message);
        Assert.Equal(200, result.StatusCode);
        Assert.Equal("13ff29e6-8508-4637-965c-0f44817c697f", result.Data!.Id.ToString());
    }

    [Fact]
    public async void AccountMovement_Success()
    {
        // Arrange
        GetAccountQueryResponse getAccountQueryResponse =
            new(new ContaCorrente("89bd9fb4-21c1-4cc3-aa79-9850bbfed7a2", 234, "Teste", true));

        AddAccountMovementCommand addAccountMovementCommand = new(
            Guid.Parse("89bd9fb4-21c1-4cc3-aa79-9850bbfed7a1"), Guid.Parse("89bd9fb4-21c1-4cc3-aa79-9850bbfed7a2"), 1,
            "C");

        AddAccountMovementQueryResponse addAccountMovementQueryResponse =
            new (Guid.Parse("67f35e6e-ac23-4886-9992-63ee0794c248"));

        IMediator? mediatr = Substitute.For<IMediator>();

        mediatr.Send(Arg.Any<GetAccountQueryRequest>()).Returns(getAccountQueryResponse);
        mediatr.Send(Arg.Any<GetIdemPotenceQueryRequest>()).Returns(new GetIdemPotenceQueryResponse(null));
        mediatr.Send(Arg.Any<AddAccountMovementQueryRequest>()).Returns(addAccountMovementQueryResponse);
        mediatr.Send(Arg.Any<AddIdempotenceQueryRequest>()).ReturnsNull();

        AddAccountMovementHandler addAccountHandler = new(mediatr);

        // Act
        AddAccountMovementCommandResponse result =
            await addAccountHandler.Handle(addAccountMovementCommand, new CancellationToken());

        // Assert
        Assert.Equal("Movimentado a conta com sucesso.", result.Message);
        Assert.Equal(200, result.StatusCode);
        Assert.Equal("67f35e6e-ac23-4886-9992-63ee0794c248", result.Data!.Id.ToString());
    }
}