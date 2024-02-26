namespace MadWorldNL.Server.Domain;

public class DefaultResponse
{
    public bool IsSuccess { get; init; }
    public string Message { get; init; } = string.Empty;

    public static DefaultResponse Success(string message = "")
    {
     return new DefaultResponse
     {
         IsSuccess = true,
         Message = message
     };   
    }

    public static DefaultResponse Error(string message)
    {
        return new DefaultResponse()
        {
            IsSuccess = false,
            Message = message
        };
    }
}