using Hangfire;
using Hangfire.PostgreSql;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Npgsql;

namespace hangfire_concurrency_poc
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddHangfire(configuration =>
            {
                var builder = new NpgsqlConnectionStringBuilder
                {
                    Host = "ubuntu.wsl",
                    Database = "hangfire",
                    Username = "hangfire",
                    Password = "123"
                };
                configuration.UsePostgreSqlStorage(builder.ConnectionString);
            });

            services.AddHostedService<Scheduler>();
            services.AddScoped<TestJob>();
        }

        public void Configure(IApplicationBuilder appBuilder)
        {
            GlobalConfiguration.Configuration.UseActivator(new HangfireActivator(appBuilder.ApplicationServices));

            appBuilder.UseHangfireServer();
        }
    }
}
