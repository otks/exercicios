using System.Data;
using Dapper;
using MediatR;
using Questao5.Domain.Entities;
using Questao5.Infrastructure.Context;
using Questao5.Infrastructure.Database.QueryStore.Requests;
using Questao5.Infrastructure.Database.QueryStore.Responses;

namespace Questao5.Infrastructure.Database.QueryStore.Handlers;

public class GetIdemPotenceQueryHandler :
    IRequestHandler<GetIdemPotenceQueryRequest, GetIdemPotenceQueryResponse>
{
    private readonly SqliteContext _context;

    public GetIdemPotenceQueryHandler(SqliteContext context)
    {
        _context = context;
    }

    public async Task<GetIdemPotenceQueryResponse> Handle(GetIdemPotenceQueryRequest request,
        CancellationToken cancellationToken)
    {
        using IDbConnection connection = _context.CreateConnection();

        const string sql = "SELECT * FROM idempotencia WHERE chave_idempotencia = @Id";
        IdemPotencia idemPotence = await connection.QueryFirstOrDefaultAsync<IdemPotencia>(sql, new
        {
            Id = request.RequestId
        });

        return new GetIdemPotenceQueryResponse(idemPotence);
    }
}