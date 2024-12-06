var builder = DistributedApplication.CreateBuilder(args);

builder.AddProject<Projects.Okala_Exchange_Web>("web");

builder.Build().Run();
