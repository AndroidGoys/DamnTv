namespace TvApi.Exceptions;

public abstract class TvException(
    string? message = null, 
    Exception? innerException = null
) : Exception(message, innerException);
