using HRApplication.Data;
using HRApplication.Models;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HRApplication.SignalR
{
    public class LeaveHub : Hub
    {
        public AppDbContext AppDbContext { get; set; }
        public LeaveHub(AppDbContext appDbContext)
        {
            AppDbContext = appDbContext;
        }

        public async Task SendNotification(string id)
        {
            Notification notification = new Notification()
            {
                sender_id = id,
                role_id = 1,
                created_at = DateTime.Now
            };
            AppDbContext.Notification.Add(notification);
            AppDbContext.SaveChanges();
            await Clients.All.SendAsync("GotMessage2",id);
        }
    }
}
