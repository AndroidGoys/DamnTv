namespace TvShowsFrontend.Client.Widgets.Models
{
    public record struct SharingParameters(
        int ChannelId,
        int? Limit,
        long? TimeStart,
        float? TimeZone
    ){
        public NormalizedSharingParameters Normalize() 
        {
            DateTimeOffset timeStart;
            if (TimeStart.HasValue)
                timeStart = DateTimeOffset.FromUnixTimeSeconds(TimeStart.Value);
            else
                timeStart = DateTimeOffset.MinValue;

            int limit = Limit ?? 4  ;

            TimeSpan timeZoneOffset;

            if (TimeZone.HasValue)
                timeZoneOffset = TimeSpan.FromHours(TimeZone.Value);
            else 
                timeZoneOffset = TimeSpan.Zero;

            timeStart = new DateTimeOffset(
                timeStart.DateTime + timeZoneOffset,
                timeZoneOffset
            );

            return new(ChannelId, limit, timeStart);
        }
    };

    public record NormalizedSharingParameters(
        int ChannelId,
        int Limit,
        DateTimeOffset TimeStart
    );
}
