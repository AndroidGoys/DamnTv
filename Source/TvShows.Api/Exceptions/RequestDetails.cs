namespace TvApi.Exceptions;

public readonly record struct RequestDetails(
    HttpRequestMessage Request,
    HttpResponseMessage Response
);
