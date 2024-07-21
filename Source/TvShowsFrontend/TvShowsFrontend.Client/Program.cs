using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Logging;
using TvApi;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.Logging.SetMinimumLevel(LogLevel.Debug);

builder.Services.AddTransient<HttpClient>();
builder.Services.AddSingleton<MinimalTvApiClient>();

await builder.Build().RunAsync();
