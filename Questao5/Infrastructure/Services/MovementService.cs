using Microsoft.Data.Sqlite;
using Questao5.Application.Commands.Requests;
using Questao5.Application.Handlers;
using Questao5.Domain.Entities;
using Questao5.Domain.Enumerators;
using Questao5.Infrastructure.Repositories.Interfaces;
using Questao5.Infrastructure.Services.Interfaces;

namespace Questao5.Infrastructure.Services;

public class MovementService : IMovementService
{
    private readonly IMovementRepository _movementRepository;
    private readonly IAccountRepository _accountRepository;

    public MovementService(IMovementRepository movementRepository,
        IAccountRepository accountRepository)
    {
        _movementRepository = movementRepository;
        _accountRepository = accountRepository;
    }

    public async Task<ReturnPattern<string>> MakeMovement(MakeMovementCommand request)
    {
            var contaCorrente = await _accountRepository.GetAccount(request.AccountId);
            if (contaCorrente is null)
                return ReturnPattern<string>.ErrorResponse("Apenas contas correntes cadastradas podem realizar movimentações",
                    "INVALID_ACCOUNT");

            if (request.Amount <= 0)
                return ReturnPattern<string>.ErrorResponse("Apenas valores positivos podem ser recebidos", "INVALID_VALUE");

            if (request.MovementType != TypesOfMovement.C && request.MovementType != TypesOfMovement.D)
                return ReturnPattern<string>.ErrorResponse("Apenas os tipos 'crédito' ou 'débito' podem ser aceitos",
                    "INVALID_TYPE");

            var movement = new Movement(request);
            await _movementRepository.AddMovimentoAsync(movement);

            return ReturnPattern<string>.SuccessResponse(movement.IdMovement);
    }
}