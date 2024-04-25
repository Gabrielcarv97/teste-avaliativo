using System.Data;
using Dapper;
using Questao5.Application.Queries.Responses;
using Questao5.Domain.Entities;
using Questao5.Infrastructure.Repositories.Scripts;

namespace Questao5.Infrastructure.Repositories.Interfaces;

public class AccountRepository : IAccountRepository
{
    private readonly IDbConnection _connection;

    public AccountRepository(IDbConnection connection)
    {
        _connection = connection;
    }

    public async Task<CurrentAccount> GetAccount(string accountId)
    {
        var account = await _connection.QueryFirstOrDefaultAsync<CurrentAccount>(MovementScripts.GetAccount, new { AccountId = accountId });
        return account;
    }
}