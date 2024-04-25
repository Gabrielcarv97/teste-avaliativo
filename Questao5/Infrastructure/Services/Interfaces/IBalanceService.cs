using Questao5.Application.Handlers;
using Questao5.Application.Queries.Responses;

namespace Questao5.Infrastructure.Services.Interfaces;

public interface IBalanceService
{
    Task<ReturnPattern<BalanceResponse>> GetBalance(string accountId);
}