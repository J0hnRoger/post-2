using Microsoft.AspNetCore.Identity;
using Practical.Notification.Application.DAO;

namespace Practical.Notification.Infrastructure.Identity;

public class ApplicationUser : IdentityUser 
{
    public List<AspNetUserNotification> Notifications { get; set; }
}
