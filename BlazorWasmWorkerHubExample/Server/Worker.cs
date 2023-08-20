
using Microsoft.AspNetCore.SignalR;
using System.Collections.Concurrent;

namespace BlazorWasmWorkerHubExample.Server
{
    public class Worker : BackgroundService
    {
        private readonly IHubContext<ChatHub, IChatHub> ChatHub;
        private readonly ILogger<Worker> logger;
        private readonly ConcurrentQueue<string> Queue = new ConcurrentQueue<string>();
        public Worker(IHubContext<ChatHub, IChatHub> chatHub, ILogger<Worker> logger)
        {
            ChatHub = chatHub;
            this.logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while(!stoppingToken.IsCancellationRequested)
            {
                //await ChatHub.Clients.All.SendMessage("Worker", DateTime.Now.ToString());
                logger.LogInformation("Working...");
                if (Queue.TryDequeue(out string? message))
                {
                    await ChatHub.Clients.All.SendMessage("Worker", $"Task Done: {message}");
                }
                else
                {
                    await ChatHub.Clients.All.SendMessage("Worker", $"No Tasks");
                }

                if(Queue.IsEmpty)
                    await Task.Delay(1000, stoppingToken);

            }
        }

        public async void DoTask(string message)
        {
            Queue.Enqueue(message);
            await Task.CompletedTask;
        }
    }
}
