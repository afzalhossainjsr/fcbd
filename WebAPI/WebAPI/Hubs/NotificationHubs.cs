using DAL.Repository.Hubs;
using Microsoft.AspNetCore.SignalR;
using Model.Hubs;

namespace WebAPI.Hubs
{
    public class NotificationHubs : Hub
    {
        private readonly IConnectedUserDAL _connectedUserDAL;

        public NotificationHubs(IConnectedUserDAL connectedUserDAL)
        {
            _connectedUserDAL = connectedUserDAL;
        }
        public async Task SendBookingNotification(string message)
        {
            await Clients.All.SendAsync("ReceiveMessage", message);
        }
        //public override async Task OnConnectedAsync()
        //{
        //    var ConnectionId = Context.ConnectionId;
        //    // Retrieve the username from the query parameter
        //    var userName = Context.GetHttpContext().Request.Query["username"];

        //    ConnectedUserModel model = new ConnectedUserModel()
        //    {
        //        HubConnectionId = ConnectionId,
        //        UserName = userName
        //    };
        //    var result = await _connectedUserDAL.SaveHubConnection(model);
        //    await base.OnConnectedAsync();
        //}
        //public override async Task OnDisconnectedAsync(Exception exception)
        //{
        //    var ConnectionId = Context.ConnectionId;
        //    ConnectedUserModel model = new ConnectedUserModel()
        //    {
        //        HubConnectionId = ConnectionId
        //    };
        //    var result = await _connectedUserDAL.DeleteHubConnection(model);

        //    await base.OnDisconnectedAsync(exception);
        //}


    }
   
}