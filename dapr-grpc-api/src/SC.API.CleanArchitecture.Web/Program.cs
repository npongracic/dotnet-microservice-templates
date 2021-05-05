using SC.API.CleanArchitecture.Infrastructure.Persistence;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Serilog;
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Server.Kestrel.Core;

namespace SC.API.CleanArchitecture.API
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            Log.Logger = new LoggerConfiguration()
                        .MinimumLevel.Verbose()
                        .Enrich.FromLogContext()
                        .Enrich.WithCorrelationId()
                        .WriteTo.Console()
                        .CreateLogger();
            try
            {
                Log.Information("Application Starting.");
                var host = CreateHostBuilder(args).Build();
                if(Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == Environments.Development)
                {
                    await host.MigrateDatabase();
                }
                
                await host.RunAsync();
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "The Application failed to start.");
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args)
        {
            return Host.CreateDefaultBuilder(args)
                .UseSerilog((hostingContext, loggerConfig) =>
                    loggerConfig.ReadFrom
                    .Configuration(hostingContext.Configuration)
                )
                .ConfigureWebHostDefaults(webBuilder => {

                    webBuilder.UseStartup<Startup>();
                    webBuilder.UseShutdownTimeout(TimeSpan.FromSeconds(15));
                    webBuilder.ConfigureKestrel(options =>
                    {
                        var grpcPort = Environment.GetEnvironmentVariable("DAPR_GRPC_PORT");
                        // Setup a HTTP/2 endpoint without TLS.
                        options.ListenLocalhost(5070, o => o.Protocols = HttpProtocols.Http2);
                        options.ListenAnyIP(5006, o => o.Protocols = HttpProtocols.Http1);
                    });
                });
        }
    }
}
