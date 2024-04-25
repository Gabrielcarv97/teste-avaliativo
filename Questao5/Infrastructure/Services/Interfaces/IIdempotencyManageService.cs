using Questao5.Application.Commands.Requests;
using Questao5.Application.Handlers;
using Questao5.Domain.Entities;

namespace Questao5.Infrastructure.Services.Interfaces;

public interface IIdempotencyManageService
{
  Task<ReturnPattern<string>> VerifyIdempotency(string idempotencyKey);   
  Task SaveIdempotency(MakeMovementCommand request, ReturnPattern<string> response, string idempotencyKey);
}