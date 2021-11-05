#region Info
// FileName:    NotifyHub.cs
// Author:      Ladislav Filip
// Created:     05.11.2021
#endregion

using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace Server.Infratructure
{
    public class NotifyHub : Hub
    {
        public async Task SendMessage(string message)
        {
            await Clients.All.SendAsync(method: "message", "anonymous", "Date");
        }
    }
}