using Aspire.Hosting;

using Projects;

IDistributedApplicationBuilder builder = DistributedApplication.CreateBuilder(args);

var previewsDesign = builder.AddProject<DamnTv_Frontend_PreviewDesign>("previewDesign");

builder.AddProject<DamnTv_Frontend>("frontend")
    .WithReference(previewsDesign);

builder.AddProject<DamnTv_TgMiniApp>("tgMiniApp");

DistributedApplication app = builder.Build();
app.Run();
