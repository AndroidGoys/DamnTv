using System.Text.Json.Serialization;

using DamnTv.Api.Client.Models;

namespace DamnTv.Api.Client.Entities;

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
            TimeStart = new(timeStart.DateTime + timeZone, timeZone),
            TimeStop = new(timeStop.DateTime + timeZone, timeZone),
            TotalCount = model.TotalCount,
            Releases = model.Releases
                .Select(x => TvChannelRelease.FromModel(x, timeZone))
                .ToList()
        };
    }
}
