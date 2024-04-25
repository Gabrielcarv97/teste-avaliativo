using MediatR;
using Questao5.Application.Handlers;

namespace Questao5.Application.Queries.Responses;

public class BalanceResponse
{
    public string AccountId { get; set; }
    public string AccountHolderName { get; set; }
    public decimal Balance { get; set; }
    public DateTime ResponseDateTime { get; set; }
}