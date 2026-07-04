using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace Admin.Blazor.Hubs
{
    [Authorize]
    public class AdminHub : Hub
    {
        public async Task BroadcastCategoryChanged()
        {
            await Clients.Others.SendAsync("CategoryChanged");
        }

        public async Task BroadcastProductChanged()
        {
            await Clients.Others.SendAsync("ProductChanged");
        }
    }
}
