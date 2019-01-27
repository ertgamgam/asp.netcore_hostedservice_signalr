using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace HostedService
{
    public class MessagesHostedService:IHostedService, IDisposable
    {
        private Timer timer;
        IServiceProvider Services { get; }

        public MessagesHostedService(IServiceProvider services)
        {
            this.Services = services;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            timer = new Timer(DoWork, null, TimeSpan.Zero, TimeSpan.FromSeconds(5));
            return Task.CompletedTask;
        }

        private void DoWork(object state)
        {       
            using (var scoped = this.Services.CreateScope())
            {
                var publishMessageHubService = scoped.ServiceProvider.GetRequiredService<IHubContext<MessagesHub>>();
                publishMessageHubService.Clients.All.SendAsync("getSendMessage", MessageProvider.GetSongMameAsMesssage());
            }
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            Debug.WriteLine("Hosted service was stopped...");
            this.timer.Change(Timeout.Infinite, 0);
            return Task.CompletedTask;
        }

        public void Dispose()
        {
            this.timer.Dispose();
            Debug.WriteLine("Hosted service was disposed...");
        }
    }
}
