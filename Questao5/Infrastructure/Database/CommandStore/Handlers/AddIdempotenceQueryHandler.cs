using System.Data;
using Dapper;
using MediatR;
using Questao5.Infrastructure.Context;
using Questao5.Infrastructure.Database.CommandStore.Requests;
using Questao5.Infrastructure.Database.CommandStore.Responses;

namespace Questao5.Infrastructure.Database.CommandStore.Handlers;

public class AddIdempotenceQueryHandler :
    IRequestHandler<AddIdempotenceQueryRequest, AddIdempotenceQueryResponce>
{
    private readonly SqliteContext _context;

    public AddIdempotenceQueryHandler(SqliteContext context)
    {
        _context = context;
    }

    public async Task<AddIdempotenceQueryResponce> Handle(AddIdempotenceQueryRequest request,
        CancellationToken cancellationToken)
    {
        using IDbConnection connection = _context.CreateConnection();

        const string sql = "INSERT INTO idempotencia (chave_idempotencia, requisicao, resultado) " +
                           "VALUES (@Id, @Request, @Result)";
        await connection.ExecuteAsync(sql, new
        {
            Id = request.RequestId,
            Request = request.Request,
            Result = request.Response
        });

        return new AddIdempotenceQueryResponce();
    }
}