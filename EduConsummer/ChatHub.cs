using Microsoft.AspNetCore.SignalR;

namespace EduConsummer
{
    public class ChatHub : Hub
    {
        public async Task SendMessage(string name, string message)
        {
            // Call the addNewMessageToPage method to update clients.
            await Clients.All.SendAsync("ReceiveMessage", name, message);
        }
    }
}
