namespace Rgs.Dms.Api.Infrastructure.BackgroundTask
{
    public interface IBackgroundTaskQueue
    {
        // Enqueues the given task.
        void EnqueueTask(Func<CancellationToken, Task> task);

        // Dequeues and returns one task. This method blocks until a task becomes available.
        Task<Func<CancellationToken, Task>> DequeueAsync(CancellationToken cancellationToken);
    }
}
