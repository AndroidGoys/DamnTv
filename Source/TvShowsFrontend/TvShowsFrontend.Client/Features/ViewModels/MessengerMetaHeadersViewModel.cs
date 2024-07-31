namespace TvShowsFrontend.Client.Features.ViewModels;

public record MessengerMetaHeadersViewModel(
    string? SiteName,
    string? Title,
    string? Description,
    string? Image,
    int ImageWidth = 1200,
    int ImageHeight = 600
);