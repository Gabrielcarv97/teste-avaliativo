using Questao5.Domain.Entities;

namespace Questao5.Infrastructure.Repositories.Interfaces;

public interface IAccountRepository
{
    Task<CurrentAccount> GetAccount(string accountId);
}