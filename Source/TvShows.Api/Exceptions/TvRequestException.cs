namespace TvApi.Exceptions;

public class TvRequestException(
    RequestDetails details,
    string? message = null, 
    Exception? innerException = null
) : TvException(message, innerException) 
{
    public RequestDetails Details { get; } = details;
}
