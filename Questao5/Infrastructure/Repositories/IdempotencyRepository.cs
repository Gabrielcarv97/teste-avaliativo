using System.Data;
using Dapper;
using Questao5.Domain.Entities;
using Questao5.Infrastructure.Repositories.Interfaces;

namespace Questao5.Infrastructure.Repositories.Scripts;

public class IdempotencyRepository : IIdempotencyRepository
{
    private readonly IDbConnection _connection;

    public IdempotencyRepository(IDbConnection connection)
    {
        _connection = connection;
    }

    public async Task<Idempotency?> GetIdempotency(string idempotencyKey)
    {
        return await _connection.QueryFirstOrDefaultAsync<Idempotency>(MovementScripts.GetIdempotency, new { IdempotencyKey = idempotencyKey });
    }

    public async Task SaveIdempotency(Idempotency idempotency)
    {
        await _connection.ExecuteAsync(MovementScripts.InsertIdempotency, new
        {
            Key = idempotency.Key,
            Request = idempotency.Request,
            Result = idempotency.Result
        });
    }
}