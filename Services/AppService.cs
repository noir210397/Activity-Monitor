using Activity_Monitor.Events;
using Activity_Monitor.Hubs;
using Microsoft.AspNetCore.SignalR;

namespace Activity_Monitor.Services
{
    public class AppService:BackgroundService
    {
        private readonly ILogger<AppService> _logger;
        private readonly Random _random = new Random();
        private  readonly IHubContext<WorkHub> _hubContext;
        public AppService(ILogger<AppService> logger,IHubContext<WorkHub> hubContext)
        {
            _logger = logger;
            _hubContext = hubContext;
        }

        private async Task DoWorkAsync(CancellationToken token)
        {
            var range = _random.Next(0, 5);
            WorkEvent work = range switch
            {
                0 => WorkEvent.Started,
                1 => WorkEvent.Completed,
                2 => WorkEvent.Failed,
                3 => WorkEvent.Cancelled,
                4 => WorkEvent.Paused,
                _ => WorkEvent.Resumed
            };
            EventLevel level = range switch
            {
                0 => EventLevel.Info,
                1 => EventLevel.Info,
                2 => EventLevel.Error,
                3 => EventLevel.Warning,
                4 => EventLevel.Info,
                _ => EventLevel.Info

            };
            await _hubContext.Clients.All.SendAsync("ReceiveWorkEvent", new WorkEventRecord(work.ToString(), level.ToString())); 
            // delay for a while
            await Task.Delay(TimeSpan.FromSeconds(5), token); 
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {

            while (!stoppingToken.IsCancellationRequested)
            {
                await DoWorkAsync(stoppingToken);
            }
            _logger.LogInformation("App backround Service is stopping.");
        }
    }
}
