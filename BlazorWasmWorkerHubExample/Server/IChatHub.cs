namespace BlazorWasmWorkerHubExample.Server
{
    public interface IChatHub
    {
        public Task SendMessage(string user, string message);
    }
}