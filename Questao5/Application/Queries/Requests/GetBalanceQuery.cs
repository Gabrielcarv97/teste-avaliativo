using MediatR;
using Questao5.Application.Handlers;
using Questao5.Application.Queries.Responses;

namespace Questao5.Application.Queries.Requests;

public class GetBalanceQuery : IRequest<ReturnPattern<BalanceResponse>>
{
    public string AccountId { get; }

    public GetBalanceQuery(string accountId)
    {
        AccountId = accountId;
    }
}