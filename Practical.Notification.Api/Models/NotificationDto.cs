namespace Practical.Notification.Api.Models;

public class NotificationDTO
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Excerpt { get; set; }
    public string Image { get; set; }
    public string Icon { get; set; }
    public string Link { get; set; }
    public string Content { get; set; }
    public string ReadStatus { get; set; }
}
