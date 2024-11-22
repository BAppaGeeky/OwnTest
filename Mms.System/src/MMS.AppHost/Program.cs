var builder = DistributedApplication.CreateBuilder(args);

var apiService = builder.AddProject<Projects.MMSApi>("apiservice");

builder.AddProject<Projects.MMS_PWA>("webfrontend")
    .WithExternalHttpEndpoints()
    .WithReference(apiService)
    .WaitFor(apiService);

builder.Build().Run();
