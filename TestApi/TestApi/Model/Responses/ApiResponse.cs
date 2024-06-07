namespace TestApi.Model.Responses;

public class ApiResponse
{
    public bool IsSuccess { get; set; }
    public string Message { get; set; }
    public object Data { get; set; }
    public Error Error { get; set; }

    public static ApiResponse Failed(Error error)
    {
        return new ApiResponse
        {
            IsSuccess = false,
            Message = string.Empty,
            Data = null,
            Error = error
        };
    }

    public static ApiResponse Success(string message, object data)
    {
        return new ApiResponse
        {
            IsSuccess = true,
            Message = message,
            Data = data,
            Error = null
        };
    }
}

public class Error
{
    public int? ErrorCode { get; set; }
    public string Message { get; set; }

    public Error(int errorCode, string messege)
    {
        ErrorCode = errorCode;
        Message = messege;
    }
}