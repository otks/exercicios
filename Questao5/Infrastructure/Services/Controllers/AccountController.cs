using MediatR;
using Microsoft.AspNetCore.Mvc;
using Questao5.Application.Commands.Requests.Account;
using Questao5.Application.Commands.Requests.Account.AccountMovement;
using Questao5.Application.Commands.Responses.Account.AccountMovement.AddAccountMovement;
using Questao5.Application.Commands.Responses.Account.GetAccountBalance;

namespace Questao5.Infrastructure.Services.Controllers;

[Route("api/account")]
[ApiController]
public class AccountController : ControllerBase
{
    private readonly IMediator _mediator;

    public AccountController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    /// Obtém o saldo da conta
    /// </summary>
    /// <param name="accountId">Id da conta do usuário</param>
    /// <returns>Retorna o saldo da conta junto com o nome, número e data </returns>
    /// <response code="200">Retorna o saldo</response>
    [HttpGet]
    [ProducesResponseType(typeof(GetAccountBalanceCommandResponse), 200)]
    public async Task<ActionResult<GetAccountBalanceCommandResponse>> GetAccountBalance(Guid accountId)
    {
        var command = new GetAccountBalanceCommandRequest(accountId);
        GetAccountBalanceCommandResponse response = await _mediator.Send(command);
        return StatusCode(response.StatusCode, response);
    }
    
    /// <summary>
    /// Adiciona uma movimentação na conta do usuário
    /// </summary>
    /// <param name="command"></param>
    /// <returns>Retorna o id da movimentação adicionada</returns>
    /// <response code="200">Retorna o id da movimentação</response>
    [HttpPost]
    [Route("movement")]
    public async Task<ActionResult<AddAccountMovementCommandResponse>> AddAccountMovement(AddAccountMovementCommand command)
    {
        AddAccountMovementCommandResponse response = await _mediator.Send(command);
        return StatusCode(response.StatusCode, response);
    }
}