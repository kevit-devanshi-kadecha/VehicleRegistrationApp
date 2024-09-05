using Microsoft.Extensions.Logging;
using Quartz;

namespace VehicleRegistration.Worker
{
    public class DeleteProfileImagesJob : IJob
    {
        private readonly ILogger<DeleteProfileImagesJob> _logger;    

        public DeleteProfileImagesJob(ILogger<DeleteProfileImagesJob> logger)
        {
            _logger = logger;
        }
        public async Task Execute(IJobExecutionContext context)
        {
            _logger.LogInformation($"Delete ProfileImages job running {DateTime.UtcNow}");
            try
            {
                var imageDirectory = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "ProfileImages");
                if (Directory.Exists(imageDirectory))
                {
                    var files = Directory.GetFiles(imageDirectory);
                    foreach (var file in files)
                    {
                        File.Delete(file);
                        _logger.LogInformation("Delete ProfileImages job executed successfully");
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occured by running the job");
            }
        }
    }
}