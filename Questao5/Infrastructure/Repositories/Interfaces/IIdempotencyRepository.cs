using Questao5.Domain.Entities;

namespace Questao5.Infrastructure.Repositories.Interfaces;

public interface IIdempotencyRepository
{
    Task<Idempotency?> GetIdempotency(string idempotencyKey);
    Task SaveIdempotency(Idempotency idempotency);
}