using System.Data;
using Dapper;
using MediatR;
using Questao5.Domain.Entities;
using Questao5.Infrastructure.Context;
using Questao5.Infrastructure.Database.QueryStore.Requests;
using Questao5.Infrastructure.Database.QueryStore.Responses;

namespace Questao5.Infrastructure.Database.QueryStore.Handlers;

public class GetAccountQueryHandler:
    IRequestHandler<GetAccountQueryRequest, GetAccountQueryResponse>
{
    private readonly SqliteContext _context;

    public GetAccountQueryHandler(SqliteContext context)
    {
        _context = context;
    }

    public async Task<GetAccountQueryResponse> Handle(GetAccountQueryRequest request, CancellationToken cancellationToken)
    {
        using IDbConnection connection = _context.CreateConnection();

        const string sql = "SELECT * FROM contacorrente WHERE idcontacorrente = @Id";
        ContaCorrente? account = await connection.QueryFirstOrDefaultAsync<ContaCorrente>(sql, new
        {
            Id = request.AccountId
        });

        return new GetAccountQueryResponse(account);
    }
}