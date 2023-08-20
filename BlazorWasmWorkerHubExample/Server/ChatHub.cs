using Microsoft.AspNetCore.SignalR;

namespace BlazorWasmWorkerHubExample.Server
{
    public class ChatHub : Hub<IChatHub>
    {
        private readonly Worker Worker;

        public ChatHub(Worker worker)
        {
            Worker = worker;
        }



        public async Task SendMessage(string user, string message)
        {
            if(string.IsNullOrWhiteSpace(message)) { return; }
            if (string.IsNullOrWhiteSpace(user)) user = "Anonymus";

            if (message.StartsWith("!"))
            {
                Worker.DoTask(message);
            }
            else
            {
                await Clients.All.SendMessage(user, message);
            }
        }
    }
}
