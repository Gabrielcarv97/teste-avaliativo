using Questao5.Domain.Enumerators;
using System;
using Questao5.Application.Commands.Requests;

namespace Questao5.Infrastructure.Database.QueryStore.Responses
{
    public class MovementResponse
    {
        public string IdMovement { get; set; }
        public string IdCurrentAccount { get; set; }
        public string MovementDate { get; set; }
        public TypesOfMovement TypeOfMovement { get; set; }
        public decimal Value { get; set; }
    }
}