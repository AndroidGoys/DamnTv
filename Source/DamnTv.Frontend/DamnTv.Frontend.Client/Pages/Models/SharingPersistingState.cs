using DamnTv.Api.Client.Entities;

namespace DamnTv.Frontend.Client.Pages.Models
{
    public class SharingPersistingState
    {
        public SharingPersistingState(ChannelDetails? channelDetails, TvReleases? channelReleases)
        {
            ChannelDetails = channelDetails;
            ChannelReleases = channelReleases;
        }

        public ChannelDetails? ChannelDetails { get; }
        public TvReleases? ChannelReleases { get; }
    }
}
