using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using Practical.Notification.Application.DAO;

namespace Practical.Notification.Application.Interface;

public interface IPortalContext
{
    DbSet<Tenant> Tenant { get; set; }
    DbSet<BmmCgs> Cgs { get; set; }
    DbSet<Translation> Translations { get; set; }
    DbSet<DAO.Notification> Notifications { get; set; }
    DbSet<AspNetUserNotification> AspNetUserNotifications { get; set; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}

/// <summary>
/// Other stuff on the WebApp 
/// </summary>
public class Tenant
{
    [Key]
    public int Id { get; set; }
}

public class Translation
{
    [Key]
    public int Id { get; set; }
}

public class BmmCgs
{
    [Key]
    public int Id { get; set; }    
}

