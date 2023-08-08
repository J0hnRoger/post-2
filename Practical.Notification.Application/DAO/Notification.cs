using System.ComponentModel.DataAnnotations.Schema;

namespace Practical.Notification.Application.DAO;

    /// <summary>
    /// Représente la table gérant les notifications
    /// </summary>
    [Table("Notifications")]
    public class Notification
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Excerpt { get; set; }
        public string? Image { get; set; }
        public string? Icon { get; set; }
        public string? Link { get; set; }
        public string? Content { get; set; }
    }
