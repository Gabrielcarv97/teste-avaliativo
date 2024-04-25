namespace Questao5.Domain.Entities;

public class Idempotency
{
    public string Key { get; set; } 
    public string Request { get; set; } 
    public string Result { get; set; } 
}