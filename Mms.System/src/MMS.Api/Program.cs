using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;

var builder = WebApplication.CreateBuilder(args);

// Load configuration
var configuration = builder.Configuration;

// Configure Kestrel to use HTTP and HTTPS
builder.WebHost.ConfigureKestrel(options =>
{
    // HTTP Binding
    var httpPort = configuration.GetValue<int>("HttpPort");
    options.Listen(System.Net.IPAddress.Any, httpPort); // Bind to all IPs for HTTP (suitable for reverse proxy or direct access)

    // HTTPS Binding
    var httpsPort = configuration.GetValue<int>("HttpsPort");
    var certPath = configuration.GetValue<string>("Kestrel:Certificates:Default:Path");
    var certPassword = configuration.GetValue<string>("Kestrel:Certificates:Default:Password");

    // Bind HTTPS only if certificate details are provided
    if (!string.IsNullOrWhiteSpace(certPath) && !string.IsNullOrWhiteSpace(certPassword))
    {
        options.Listen(System.Net.IPAddress.Any, httpsPort, listenOptions =>
        {
            listenOptions.UseHttps(certPath, certPassword);
        });
        Console.WriteLine($"HTTPS configured on port {httpsPort} using certificate at {certPath}.");
    }
    else
    {
        Console.WriteLine("HTTPS is not configured. Ensure certificate path and password are set in appsettings.json for production.");
    }
});

// Build the app
var app = builder.Build();

// Add a basic health check endpoint
app.MapGet("/health", () => Results.Ok("API is healthy"));

// Run the app
await app.RunAsync();
