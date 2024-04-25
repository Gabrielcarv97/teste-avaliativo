using System.Data;
using Questao5.Application.Queries.Responses;
using Questao5.Infrastructure.Repositories.Interfaces;
using Dapper;
using Questao5.Domain.Entities;
using Questao5.Infrastructure.Repositories.Scripts;
using Questao5.Infrastructure.Database.QueryStore.Responses;

namespace Questao5.Infrastructure.Repositories;

public class MovementRepository : IMovementRepository
{
    private readonly IDbConnection _connection;

    public MovementRepository(IDbConnection connection)
    {
        _connection = connection;
    }

    public async Task AddMovimentoAsync(Movement movement)
    {
            var parameters = new DynamicParameters();
            parameters.Add("@IdMovement", movement.IdMovement, DbType.String);
            parameters.Add("@IdCurrentAccount", movement.IdCurrentAccount, DbType.String);
            parameters.Add("@MovementDate", movement.MovementDate, DbType.String);
            parameters.Add("@TypeOfMovement", movement.TypeOfMovement.ToString(), DbType.String);
            parameters.Add("@Value", movement.Value, DbType.Decimal);
            
            await _connection.ExecuteAsync(MovementScripts.InsertMovements, parameters); 
    }

    public async Task<IEnumerable<MovementResponse>> GetBalance(string accountId)
    {
        var balance = await _connection.QueryAsync<MovementResponse>(MovementScripts.GetMovements, new { AccountId = accountId });

        return balance;
    }
}