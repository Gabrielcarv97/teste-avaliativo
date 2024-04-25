namespace Questao5.Infrastructure.Repositories.Scripts;

public static class MovementScripts
{
    public static readonly string GetAccount = @"
        SELECT idcontacorrente AS IdCurrentAccount,
            numero AS Number,
            nome AS Name,
            ativo AS Active
        FROM ContaCorrente
        WHERE idcontacorrente = @AccountId";

    public static readonly string InsertMovements = @"
    INSERT INTO movimento (idmovimento, idcontacorrente, datamovimento, tipomovimento, valor)
    VALUES (@IdMovement, @IdCurrentAccount, @MovementDate, @TypeOfMovement, @Value);
";

    public static readonly string GetIdempotency = @"
        SELECT chave_idempotencia AS Key,
            requisicao AS Request,
            resultado AS Result
        FROM idempotencia
        WHERE chave_idempotencia = @IdempotencyKey";

    public static readonly string InsertIdempotency = @"
    INSERT INTO idempotencia (chave_idempotencia, requisicao, resultado)
    VALUES (@Key, @Request, @Result);
";

    public static readonly string GetMovements = @"
        SELECT idmovimento AS IdMovement,
            idcontacorrente AS IdCurrentAccount,
            datamovimento AS MovementDate,
            tipomovimento AS TypeOfMovement,
            valor AS Value
        FROM movimento
        WHERE idcontacorrente = @AccountId";
}