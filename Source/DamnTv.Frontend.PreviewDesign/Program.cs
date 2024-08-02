using DamnTv.Api.Client;
using DamnTv.Frontend.PreviewDesign.Models;
using DamnTv.Frontend.PreviewDesign.Routes;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();

builder.Services.AddHttpClient();
builder.Services.AddSingleton<MinimalTvApiClient>();
builder.Services.AddTransient<IPreviewBuilder, SkiaPreviewBuilder>();

WebApplication app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
}

app.UseRouting();
app.MapPreviewsDistribution();
await app.RunAsync();
