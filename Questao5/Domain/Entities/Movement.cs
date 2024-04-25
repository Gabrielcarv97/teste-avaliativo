using Questao5.Domain.Enumerators;
using System;
using Questao5.Application.Commands.Requests;

namespace Questao5.Domain.Entities
{
    public class Movement
    {
        public string IdMovement { get; set; }
        public string IdCurrentAccount { get; set; }
        public string MovementDate { get; set; }
        public TypesOfMovement TypeOfMovement { get; set; }
        public decimal Value { get; set; }
        
        public Movement(MakeMovementCommand movementCommand)
        {
            IdMovement = Guid.NewGuid().ToString();
            IdCurrentAccount = movementCommand.AccountId;
            MovementDate = movementCommand.Date.ToString("dd/MM/yyyy");
            TypeOfMovement = movementCommand.MovementType;
            Value = Math.Round(movementCommand.Amount, 2);
        } 

        public Movement(string idMovement, string idCurrentAccount, string movementDate, TypesOfMovement typeOfMovement, decimal value)
        {
            IdMovement = idMovement;
            IdCurrentAccount = idCurrentAccount;
            MovementDate = movementDate;
            TypeOfMovement = typeOfMovement;
            Value = value;
        }
    }
}