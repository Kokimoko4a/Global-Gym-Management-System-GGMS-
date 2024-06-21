using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

public class ChatHub : Hub
{
    public override Task OnConnectedAsync()
    {
        var userId = Context.UserIdentifier; // Assuming user ID is set as the user identifier
        Groups.AddToGroupAsync(Context.ConnectionId, userId);
        return base.OnConnectedAsync();
    }

    public override Task OnDisconnectedAsync(Exception exception)
    {
        var userId = Context.UserIdentifier;
        Groups.RemoveFromGroupAsync(Context.ConnectionId, userId);
        return base.OnDisconnectedAsync(exception);
    }

    public Task SendMessageToUser(string userId, string message, string senderId)
    {
        return Clients.Group(userId).SendAsync("ReceiveMessage", senderId, message, senderId);
    }
}
