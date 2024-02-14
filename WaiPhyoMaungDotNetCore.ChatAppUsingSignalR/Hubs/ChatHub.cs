﻿using Microsoft.AspNetCore.SignalR;

namespace WaiPhyoMaungDotNetCore.ChatAppUsingSignalR.Hubs
{
    public class ChatHub : Hub
    {
        public async Task ServerReceiveMessage(string user, string message)
        {
            await Clients.All.SendAsync("ClientReceiveMessage", user, message);
        }
    }
}
