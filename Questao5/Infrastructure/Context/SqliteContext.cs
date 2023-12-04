using System.Data;
using Microsoft.Data.Sqlite;

namespace Questao5.Infrastructure.Context;

public class SqliteContext
{
    private readonly string _connectionString;

    public SqliteContext(IConfiguration configuration)
    {
        _connectionString = configuration["DatabaseName"];
    }

    public IDbConnection CreateConnection() => new SqliteConnection(_connectionString);
}