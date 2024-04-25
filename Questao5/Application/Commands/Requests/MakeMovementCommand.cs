using MediatR;
using Microsoft.VisualStudio.TestPlatform.ObjectModel.Client;
using Questao5.Application.Handlers;
using Questao5.Domain.Enumerators;

namespace Questao5.Application.Commands.Requests;

public class MakeMovementCommand : IRequest<ReturnPattern<string>>
{
    public string AccountId { get; }
    public DateTime Date { get; }
    public TypesOfMovement MovementType { get; }
    public decimal Amount { get; }

    public MakeMovementCommand(string accountId, DateTime date, TypesOfMovement movementType, decimal amount)
    {
        AccountId = accountId;
        Date = date;
        MovementType = movementType;
        Amount = amount;
    }
}