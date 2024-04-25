namespace Questao5.Application.Handlers;

public class ExceptionManage: Exception
{
    public class NotFoundException : Exception
    {
        public string ErrorType { get; }

        public NotFoundException(string message, string errorType) : base(message)
        {
            ErrorType = errorType;
        }
    }
 
    public class BadRequestException : Exception
    {
        public string ErrorType { get; }

        public BadRequestException(string message, string errorType) : base(message)
        {
            ErrorType = errorType;
        }
    }
}