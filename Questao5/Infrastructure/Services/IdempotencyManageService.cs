using System.Text.Json;
using Polly;
using Questao5.Application.Commands.Requests;
using Questao5.Application.Handlers;
using Questao5.Domain.Entities;
using Questao5.Infrastructure.Repositories.Interfaces;
using Questao5.Infrastructure.Services.Interfaces;

namespace Questao5.Infrastructure.Services;

public class IdempotencyManageService : IIdempotencyManageService
{
    private readonly IIdempotencyRepository _idempotencyRepository;

    public IdempotencyManageService(IIdempotencyRepository idempotencyRepository)
    {
        _idempotencyRepository = idempotencyRepository;
    }

    public async Task<ReturnPattern<string>> VerifyIdempotency(string idempotencyKey)
    {
        var idempotency = await _idempotencyRepository.GetIdempotency(idempotencyKey);
        if (idempotency is not null)
        {
            var resultIdempotency = JsonSerializer.Deserialize<ReturnPattern<string>>(idempotency.Result);
            return resultIdempotency!;
        }
         return null;
    }

    public async Task SaveIdempotency(MakeMovementCommand request, ReturnPattern<string> response, string idempotencyKey)
    {
        var idempotency = new Idempotency()
        {
            Key = idempotencyKey,
            Request = JsonSerializer.Serialize(request),
            Result = JsonSerializer.Serialize(response)
        };

        var retryPolicy = Policy
       .Handle<Exception>()
       .WaitAndRetryAsync(3, retryAttempt => TimeSpan.FromSeconds(3));

        await retryPolicy.ExecuteAsync(async () => await _idempotencyRepository.SaveIdempotency(idempotency));
    } 


}