using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Practical.Notification.Api.Models;
using Practical.Notification.Application.DAO;
using Practical.Notification.Infrastructure.Identity;
using Practical.Notification.Infrastructure.Persistence;

namespace Practical.Notification.Api.Controllers;

[ApiController]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
[Route("api/[controller]")]
public class NotificationController : ControllerBase
{
    private readonly ILogger<NotificationController> _logger;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly PortalContext _ctx;
    
    public NotificationController(ILogger<NotificationController> logger, UserManager<ApplicationUser> userManager,
        PortalContext ctx)
    {
       _logger = logger;
        _userManager = userManager;
        _ctx = ctx;
    }

        /// <summary>
        /// Recupere l'ensemble des notifications d'un utilisateur.
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<List<NotificationDTO>>> GetNotifications()
        {
            ApplicationUser user = await _userManager.GetUserAsync(User);
            if (user == null)
                return Forbid("Utilisateur non-authentifié");
      
            List<NotificationDTO> userNotifications = _ctx.AspNetUserNotifications
                .Where(n => n.BmmAspNetUserId == user.Id)
                .Select(n => new NotificationDTO
                {
                    Id = n.Notification.Id,
                    Title = n.Notification.Title,
                    Excerpt = n.Notification.Excerpt,
                    Image = n.Notification.Image,
                    Icon = n.Notification.Icon,
                    Link = n.Notification.Link,
                    Content = n.Notification.Content,
                    ReadStatus = n.ReadStatus
                })
                .ToList();

            return Ok(userNotifications);
        }

        /// <summary>
        /// Recupere l'ensemble des notifications d'un utilisateur.
        /// </summary>
        [HttpPut("api/notifications/{notificationID}")]
        [AllowAnonymous]
        public async Task<ActionResult> SetNotificationsStatus(int notificationID, [FromBody] string readStatus)
        {
            ApplicationUser user = await _userManager.GetUserAsync(User);
            if (user == null)
                return Forbid("Utilisateur non-authentifié");
                
            AspNetUserNotification aspNetUserNotification = _ctx.AspNetUserNotifications
                .FirstOrDefault(n => n.BmmNotificationId == notificationID && n.BmmAspNetUserId == user.Id);

            return Ok();
        }
}