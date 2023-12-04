using System.Data;
using Dapper;
using MediatR;
using Questao5.Infrastructure.Context;
using Questao5.Infrastructure.Database.CommandStore.Requests;
using Questao5.Infrastructure.Database.CommandStore.Responses;

namespace Questao5.Infrastructure.Database.CommandStore.Handlers;

public class AddAccountMovementQueryHandler :
    IRequestHandler<AddAccountMovementQueryRequest, AddAccountMovementQueryResponse>
{
    private readonly SqliteContext _context;

    public AddAccountMovementQueryHandler(SqliteContext context)
    {
        _context = context;
    }

    public async Task<AddAccountMovementQueryResponse> Handle(AddAccountMovementQueryRequest request, CancellationToken cancellationToken)
    {
        using IDbConnection connection = _context.CreateConnection();

        const string query = "INSERT INTO movimento (idmovimento, idcontacorrente,datamovimento,tipomovimento,valor) " +
                             "VALUES (@Id, @IdConta, @DataMovimento, @TipoMovimento, @Valor)";
        
        Guid id = Guid.NewGuid();
        
        await connection.ExecuteAsync(query, new
        {
            Id = id.ToString(),
            IdConta = request.AccountId,
            DataMovimento = request.MovementDate.ToString("s"),
            TipoMovimento = request.MovementType.ToString(),
            Valor = request.Value,
        });

        return new AddAccountMovementQueryResponse(id);
    }
}