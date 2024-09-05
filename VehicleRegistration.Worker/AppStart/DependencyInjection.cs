using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VehicleRegistration.Worker.AppStart
{
    public static class DependencyInjection
    {
        public static void AddDependency(this IServiceCollection services) 
        {
            services.AddQuartz(options =>
            {
                options.UseMicrosoftDependencyInjectionJobFactory();

                var jobKey = JobKey.Create(nameof(DeleteProfileImagesJob));
                options
                    .AddJob<DeleteProfileImagesJob>(jobBuilder => jobBuilder.WithIdentity(jobKey))
                    .AddTrigger(trigger => trigger.ForJob(jobKey).WithCronSchedule(" 0 0/1 * 1/1 * ? *"));

            });
            services.AddQuartzHostedService(options =>
            {
                options.WaitForJobsToComplete = true;
            });
        }
    }
}
