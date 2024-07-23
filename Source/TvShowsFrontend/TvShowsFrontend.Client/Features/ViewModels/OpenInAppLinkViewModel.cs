namespace TvShowsFrontend.Client.Features.ViewModels;

public class OpenInAppLinkViewModel (
    int channelId, DateTimeOffset time, 
    string message = "Открыть в приложении"
)
{
    public DateTimeOffset Time { get; } = time;
    public int ChannelId { get; } = channelId;
    public string Message { get; }= message;
    public string Url => 
        $"limetv://app.show" +
            $"?time={
                Time
                .ToUniversalTime()
            .       ToUnixTimeSeconds()
                }" +
            $"&id={
                ChannelId
            }";

}
