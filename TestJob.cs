using Microsoft.Extensions.Logging;

namespace hangfire_concurrency_poc
{
    public class TestJob
    {
        private readonly ILogger<TestJob> _logger;

        public TestJob(ILogger<TestJob> logger)
        {
            _logger = logger;
        }

        public void Exec()
        {
            _logger.LogInformation("Exec job");
        }
    }
}