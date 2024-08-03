using DamnTv.Api.Client;
using DamnTv.Frontend.Client.Pages.ViewModels;

using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.Logging.SetMinimumLevel(LogLevel.Debug);

builder.Services.AddTransient<HttpClient>();
builder.Services.AddSingleton<MinimalTvApiClient>();
builder.Services.AddSingleton<ISharingViewModel, SharingViewModel>();

await builder.Build().RunAsync();
