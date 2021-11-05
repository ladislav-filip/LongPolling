#region Info

// FileName:    SendController.cs
// Author:      Ladislav Filip
// Created:     05.11.2021

#endregion

using System;
using System.Globalization;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Server.Infratructure;

namespace Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SendController : ControllerBase
    {
        private readonly IHubContext<NotifyHub> _notifyHub;

        public SendController(IHubContext<NotifyHub> notifyHub)
        {
            _notifyHub = notifyHub;
        }
        
        [HttpPost]
        public async Task<IActionResult> Send(string message)
        {
            await _notifyHub.Clients.All.SendAsync(method: "message", "server", $"{DateTime.Now.ToString(CultureInfo.InvariantCulture)}: {message}");
            return Ok();
        }
    }
}