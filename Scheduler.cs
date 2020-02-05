using System;
using System.Threading;
using System.Threading.Tasks;
using Hangfire;
using Microsoft.Extensions.Hosting;

namespace hangfire_concurrency_poc
{
    public class Scheduler : BackgroundService
    {
        private readonly IBackgroundJobClient _backgroundJobClient;

        public Scheduler(IBackgroundJobClient backgroundJobClient)
        {
            _backgroundJobClient = backgroundJobClient;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                _backgroundJobClient.Enqueue<TestJob>(job => job.Exec());
                await Task.Delay(TimeSpan.FromMinutes(1), stoppingToken);
            }
        }
    }
}