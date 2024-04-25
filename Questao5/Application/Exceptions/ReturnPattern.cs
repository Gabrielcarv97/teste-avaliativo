
namespace Questao5.Application.Handlers;

public class ReturnPattern<T>
{
    public bool Success { get; set; }
    public string Message { get; set; } 
    public T? Data { get; set; } 
    public string Type { get; set; } 
    
    public ReturnPattern(bool success, string message, T? data, string? type)
    {
        Success = success;
        Message = message; 
        Data = data;
        Type = type;
    }

    public static ReturnPattern<T> SuccessResponse(T data)
    {
        return new ReturnPattern<T>(true, "Operation successful", data, null);
    }

    public static ReturnPattern<T> ErrorResponse(string message, string type)
    {
        return new ReturnPattern<T>(false, message, default(T), type);
    }
}