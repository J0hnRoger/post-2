namespace Practical.Notification.Application.DAO;

/// </summary>
public class AspNetUserNotification
{
    public int Id { get; set; }
    public int BmmNotificationId { get; set; }
    public string BmmAspNetUserId { get; set; }
    public string ReadStatus { get; set; }
    public Notification Notification { get; set; }
}