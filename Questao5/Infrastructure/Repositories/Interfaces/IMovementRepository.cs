using Questao5.Application.Queries.Responses;
using Questao5.Domain.Entities;
using Questao5.Infrastructure.Database.QueryStore.Responses;

namespace Questao5.Infrastructure.Repositories.Interfaces;

public interface IMovementRepository
{
    Task AddMovimentoAsync(Movement movement);
    Task<IEnumerable<MovementResponse>> GetBalance(string accountId);
}