using System.ComponentModel.DataAnnotations.Schema;

namespace Practical.Notification.Application.DAO;

/// </summary>
[Table("AspNetUserNotifications")]
public class AspNetUserNotification
{
    public int Id { get; set; }
    public int NotificationId { get; set; }
    public string AspNetUserId { get; set; }
    public string ReadStatus { get; set; }
    public Notification Notification { get; set; }
}