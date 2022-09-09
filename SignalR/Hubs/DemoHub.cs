using Microsoft.AspNetCore.SignalR;
using System.Xml.Linq;

namespace SignalR.Hubs
{
    public class DemoHub : Hub
    {
        public override async Task OnConnectedAsync()
        {
            Console.WriteLine(Context.ConnectionId);

            await Clients.Caller.SendAsync("Welcome", "Welcome in RignalR");
            await Clients.Others.SendAsync("NewConnection", Context.ConnectionId);

            await base.OnConnectedAsync();
        }


        public async Task SayHello()
        {
            await Clients.Caller.SendAsync("HelloResponse", "Hello");
        }

        public async Task SayHelloToMe(string myName)
        {
            await Clients.Caller.SendAsync("HelloResponse", $"Hello {myName}");
        }

        public async Task Echo(string message)
        {
            await Clients.Caller.SendAsync("Echo", message);
        }

        public async Task SendMessage(string connectionId, string message)
        {
            await Clients.Client(connectionId).SendAsync(nameof(SendMessage), message);
        }


        public async Task JoinGroup(string groupName)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, groupName);

            await Clients.Group(groupName).SendAsync("Welcome", $"New member in group {groupName}");
        }
    }
}
