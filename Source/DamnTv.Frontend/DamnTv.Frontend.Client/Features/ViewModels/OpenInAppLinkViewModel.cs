using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Routing;
using Microsoft.AspNetCore.Components.Web;

namespace DamnTv.Frontend.Client.Features.ViewModels;

public class OpenInAppLinkViewModel
{

    public OpenInAppLinkViewModel(
        int channelId, DateTimeOffset time,
        string message = "Открыть в приложении"
    )
    {
        long unixTime = time
            .ToUniversalTime()
            .ToUnixTimeSeconds();

        Message = message;
        Url = $"limetv://app.show?time={unixTime}&id={channelId}";
        AlternativeRoute = "https://github.com/AndroidGoys/TvShowsAndroid/releases";
    }
    public string Message { get; }
    public string AlternativeRoute { get; }
    public string Url;
}
