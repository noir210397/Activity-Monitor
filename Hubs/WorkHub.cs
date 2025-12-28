using Activity_Monitor.Events;
using Microsoft.AspNetCore.SignalR;
using System.Collections.Concurrent;

namespace Activity_Monitor.Hubs
{
    public sealed class WorkHub:Hub
    {
        private static ConcurrentDictionary<string, bool> connections = new();

        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            connections.TryRemove(Context.ConnectionId, out _);
            await Clients.All.SendAsync("ClientDisconnected", new ClientEvent(connections.Count));
            await base.OnDisconnectedAsync(exception);
        }
        public override async Task OnConnectedAsync()
        {
            connections.TryAdd(Context.ConnectionId, true);
            await Clients.All.SendAsync("NewClientConnected", new ClientEvent(connections.Count));
            await base.OnConnectedAsync();
        }
    }
}
