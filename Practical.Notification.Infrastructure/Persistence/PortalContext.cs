using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Practical.Notification.Application.DAO;
using Practical.Notification.Infrastructure.Identity;

namespace Practical.Notification.Infrastructure.Persistence;
public class PortalContext : IdentityDbContext<ApplicationUser>
{
    public DbSet<Tenant> Tenant { get; set; }
    public DbSet<BmmCgs> Cgs { get; set; }
    public DbSet<Translation> Translations { get; set; }
    public DbSet<Application.DAO.Notification> Notifications { get; set; }
    public DbSet<AspNetUserNotification> AspNetUserNotifications { get; set; }

    public PortalContext(DbContextOptions<PortalContext> options)
        : base(options) { }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
    }
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
