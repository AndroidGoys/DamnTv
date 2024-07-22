namespace TvShowsFrontend.Client.Widgets.Models
{
    public record struct SharingParameters(
        int ChannelId,
        int? Limit,
        long? TimeStart,
        float? TimeZone
    );
}
