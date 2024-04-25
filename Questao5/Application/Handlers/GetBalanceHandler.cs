using MediatR;
using Questao5.Application.Queries.Requests;
using Questao5.Application.Queries.Responses;
using Questao5.Infrastructure.Services.Interfaces;

namespace Questao5.Application.Handlers;

public class GetBalanceHandler : IRequestHandler<GetBalanceQuery, ReturnPattern<BalanceResponse>>
{
    private readonly IBalanceService _balanceService;

    public GetBalanceHandler(IBalanceService balanceService)
    {
        _balanceService = balanceService;
    }

    public async Task<ReturnPattern<BalanceResponse>> Handle(GetBalanceQuery request, CancellationToken cancellationToken)
    {
        try
        {
            return await _balanceService.GetBalance(request.AccountId);
        }
        catch (Exception ex)
        {
            throw new Exception("Ocorreu um erro ao obter o saldo da conta corrente.", ex);
        }
    }
} 
