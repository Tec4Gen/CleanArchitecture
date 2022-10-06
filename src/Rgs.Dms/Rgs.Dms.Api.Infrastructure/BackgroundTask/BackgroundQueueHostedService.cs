using Microsoft.Extensions.Hosting;

namespace Rgs.Dms.Api.Infrastructure.BackgroundTask
{
    //!!!Если в фоновом потоке будет использоваться объект реализующий IDisposable, необходимо использовать IServiceScopeFactory
    //https://stackoverflow.com/questions/49813628/run-a-background-task-from-a-controller-action-in-asp-net-core

    public class BackgroundQueueHostedService : BackgroundService
    {
        private readonly IBackgroundTaskQueue _taskQueue;

        public BackgroundQueueHostedService(IBackgroundTaskQueue taskQueue)
        {
            _taskQueue = taskQueue ?? throw new ArgumentNullException(nameof(taskQueue));   
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            // Dequeue and execute tasks until the application is stopped
            while (!stoppingToken.IsCancellationRequested)
            {
                // Get next task
                // This blocks until a task becomes available
                var task = await _taskQueue.DequeueAsync(stoppingToken);

                try
                {
                    // Run task
                    await task(stoppingToken);
                }
                catch (Exception ex)
                {
                    //_logger.LogError(ex, "An error occured during execution of a background task");
                }
            }
        }
    }
}
