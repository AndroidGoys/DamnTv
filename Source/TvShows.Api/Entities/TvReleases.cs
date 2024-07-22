using System.Text.Json.Serialization;

using TvApi.Models;

namespace TvApi.Entities;

public record TvReleases
{
    public required DateTimeOffset TimeStart { get; init; }
    public required DateTimeOffset TimeStop { get; init; }
    public required int TotalCount { get; init; }
    public required IEnumerable<TvChannelRelease> Releases { get; init; }

    internal static TvReleases FromModel(TvReleasesModel model, TimeSpan timeZone) 
    {
        DateTimeOffset timeStart = DateTimeOffset.FromUnixTimeSeconds(model.TimeStart);
        DateTimeOffset timeStop = DateTimeOffset.FromUnixTimeSeconds(model.TimeStop);
        return new TvReleases()
        {
            TimeStart = new(timeStart.DateTime, timeZone),
            TimeStop = new(timeStop.DateTime, timeZone),
            TotalCount = model.TotalCount,
            Releases = model.Releases
                .Select(x => TvChannelRelease.FromModel(x, timeZone))
                .ToList()
        };
    }
}
