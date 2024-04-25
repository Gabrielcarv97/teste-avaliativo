using MediatR;
using Polly;
using Questao5.Application.Commands.Requests;
using Questao5.Infrastructure.Services.Interfaces;

namespace Questao5.Application.Handlers;

public class MovementHandler : IRequestHandler<MakeMovementCommand, ReturnPattern<string>>
{
    private readonly IMovementService _movementService;

    public MovementHandler(IMovementService movementService)
    {
        _movementService = movementService;
    }

    public async Task<ReturnPattern<string>> Handle(MakeMovementCommand request, CancellationToken cancellationToken)
    {
        var retryPolicy = Policy
            .Handle<Exception>()
            .WaitAndRetryAsync(3, retryAttempt => TimeSpan.FromSeconds(3));

        var policyResult = await retryPolicy.ExecuteAndCaptureAsync(async () => await _movementService.MakeMovement(request));

        if (policyResult.Outcome == OutcomeType.Failure)
        {
            return ReturnPattern<string>.ErrorResponse("Todas as tentativas de realizar a movimentação falharam", "MOVEMENT_FAILED");
        }

        return policyResult.Result;
    }
}