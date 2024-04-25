using Questao5.Application.Commands.Requests;
using Questao5.Application.Handlers;

namespace Questao5.Infrastructure.Services.Interfaces;

public interface IMovementService
{
    Task<ReturnPattern<string>> MakeMovement(MakeMovementCommand request);
}