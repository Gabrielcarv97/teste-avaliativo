using MediatR;
using Microsoft.AspNetCore.Mvc;
using Questao5.Application.Commands.Requests;
using Questao5.Application.Queries.Requests;
using Questao5.Infrastructure.Services.Interfaces;

namespace Questao5.Infrastructure.Services.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CurrentAccountController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly IIdempotencyManageService _idempotencyManageService;

    public CurrentAccountController(IMediator mediator, IIdempotencyManageService idempotencyManageService)
    {
        _mediator = mediator;
        _idempotencyManageService = idempotencyManageService;
    }

    [HttpGet("balance")]
    public async Task<IActionResult> Balance(string idcontacorrente)
    {
        var sendBalance = new GetBalanceQuery(idcontacorrente);
        var balance = await _mediator.Send(sendBalance);
        
        return balance.Success ? Ok(balance) : BadRequest(balance);
    }

    [HttpPost("movement")]
    public async Task<IActionResult> MakeMovement([FromBody] MakeMovementCommand request,
        [FromHeader(Name = "Idempotency-Key")] string idempotencyKey)
    {
        var idempotency = await _idempotencyManageService.VerifyIdempotency(idempotencyKey);
        if (idempotency is not null)
        {
            return idempotency.Success ? Ok(idempotency) : BadRequest(idempotency);
        }

        var response = await _mediator.Send(request);
        await _idempotencyManageService.SaveIdempotency(request, response, idempotencyKey);

        return response.Success ? Ok(response) : BadRequest(response);
    }
}