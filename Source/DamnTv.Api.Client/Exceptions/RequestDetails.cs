namespace DamnTv.Api.Client.Exceptions;

public readonly record struct RequestDetails(
    HttpRequestMessage Request,
    HttpResponseMessage Response
);
