using System.Globalization;

using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Logging;
using TvApi;

using TvShowsFrontend.Client.Pages.ViewModels;
using TvShowsFrontend.Client.Widgets.ViewModels;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.Logging.SetMinimumLevel(LogLevel.Debug);

builder.Services.AddTransient<HttpClient>();
builder.Services.AddSingleton<MinimalTvApiClient>();
builder.Services.AddSingleton<ISharingViewModel, SharingViewModel>();
builder.Services.AddTransient<ISharingWidgetViewModel, SharingWidgetViewModel>();

await builder.Build().RunAsync();
