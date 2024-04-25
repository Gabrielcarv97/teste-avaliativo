using Questao5.Application.Handlers;
using Questao5.Application.Queries.Responses;
using Questao5.Domain.Enumerators;
using Questao5.Infrastructure.Repositories.Interfaces;
using Questao5.Infrastructure.Services.Interfaces;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Questao5.Infrastructure.Services
{
    public class BalanceService : IBalanceService
    {
        private readonly IMovementRepository _movementRepository;
        private readonly IAccountRepository _accountRepository;

        public BalanceService(IMovementRepository movementRepository, IAccountRepository accountRepository)
        {
            _movementRepository = movementRepository;
            _accountRepository = accountRepository;
        }

        public async Task<ReturnPattern<BalanceResponse>> GetBalance(string accountId)
        {
            var account = await _accountRepository.GetAccount(accountId);
            if (account == null)
            {
                return ReturnPattern<BalanceResponse>.ErrorResponse("Apenas contas correntes cadastradas podem realizar movimentações", "INVALID_ACCOUNT");
            }

            if (!account.Active)
            {
                return ReturnPattern<BalanceResponse>.ErrorResponse("Conta corrente inativa", "INACTIVE_ACCOUNT");
            } 

            var movements = await _movementRepository.GetBalance(accountId);
            var sumCredit = movements.Sum(m => m.TypeOfMovement == TypesOfMovement.C ? m.Value : 0);
            var sumDebit = movements.Sum(m => m.TypeOfMovement == TypesOfMovement.D ? m.Value : 0);
            var balance = sumCredit - sumDebit;

            return ReturnPattern<BalanceResponse>.SuccessResponse(new BalanceResponse { AccountId = accountId, AccountHolderName = account.Name, Balance = balance, ResponseDateTime = DateTime.Now });
        }
    }
}