using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Practical.Notification.Application.DAO;
using Practical.Notification.Application.Interface;
using Practical.Notification.Infrastructure.Identity;

namespace Practical.Notification.Infrastructure.Persistence;

public class PortalContext : IdentityDbContext<ApplicationUser>, IPortalContext
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
