using System.Data;
using Dapper;
using MediatR;
using Questao5.Domain.Entities;
using Questao5.Infrastructure.Context;
using Questao5.Infrastructure.Database.QueryStore.Requests;
using Questao5.Infrastructure.Database.QueryStore.Responses;

namespace Questao5.Infrastructure.Database.QueryStore.Handlers;

public class GetAccountBalanceQueryHandler :
    IRequestHandler<GetAccountBalanceQueryRequest, GetAccountBalanceQueryResponse>
{
    private readonly SqliteContext _context;

    public GetAccountBalanceQueryHandler(SqliteContext context)
    {
        _context = context;
    }

    public async Task<GetAccountBalanceQueryResponse> Handle(GetAccountBalanceQueryRequest request,
        CancellationToken cancellationToken)
    {
        using IDbConnection connection = _context.CreateConnection();

        const string sql = " SELECT (SELECT sum(valor)" +
                           " FROM movimento" +
                           " WHERE idcontacorrente = @Id" +
                           " AND tipomovimento = 'C') -" +
                           " (SELECT sum(valor)" +
                           " FROM movimento" +
                           " WHERE idcontacorrente = @Id" +
                           " AND tipomovimento = 'D') as saldo, nome, numero, datetime() as dataHoraConsulta" +
                           " FROM contacorrente" +
                           " WHERE idcontacorrente = @Id";

        SaldoConta balance = await connection.QueryFirstOrDefaultAsync<SaldoConta>(sql, new
        {
            Id = request.AccountId.ToString().ToUpper()
        });

        return new GetAccountBalanceQueryResponse(balance);
    }
}