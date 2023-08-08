using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Practical.Notification.Application.DAO;
using Practical.Notification.Infrastructure.Identity;

namespace Practical.Notification.Infrastructure.Persistence;

public class PortalContextInitialiser
{
    private readonly ILogger<PortalContextInitialiser> _logger;
    private readonly PortalContext _context;
    private readonly UserManager<ApplicationUser> _userManager;

    public PortalContextInitialiser(ILogger<PortalContextInitialiser> logger, PortalContext context, UserManager<ApplicationUser> userManager)
    {
        _logger = logger;
        _context = context;
        _userManager = userManager;
    }

    public async Task InitialiseAsync()
    {
        try
        {
            await _context.Database.EnsureCreatedAsync();
            await TrySeedAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while initialising the database.");
            throw;
        }
    }

    public async Task TrySeedAsync()
    {
        // Default users
        var tester = new ApplicationUser { UserName = "tester@local.com", Email = "tester@local.com" };
        var existing = await _userManager.FindByEmailAsync(tester.Email);
        if (existing == null)
        {
            var result = await _userManager.CreateAsync(tester, "Test1!");
            if (!result.Succeeded)
                throw new Exception($"Error creating tester user: {String.Join(",", result.Errors)}.");
            existing = tester;
        }
    
        // Default data
        // Seed, if necessary
        if (!_context.Notifications.Any())
        {
            var newVersionNotification = new Application.DAO.Notification() 
            {
                Title = "New version available",
                Excerpt = "A new version of the application is available. Please refresh your browser.",
            };
            
            _context.Notifications.Add(newVersionNotification);
            var maintenanceVersion = new Application.DAO.Notification()
            {
                Title = "Application maintenance scheduled",
                Excerpt = "The application will be unavailable on 1st September 2023 between 00:00 and 01:00 for scheduled maintenance."
            }; 
            
            _context.Notifications.Add(maintenanceVersion);

            _context.AspNetUserNotifications.Add(new AspNetUserNotification()
            {
                AspNetUserId = existing.Id,
                Notification = newVersionNotification,
                ReadStatus = "not read"
            });
            
            await _context.SaveChangesAsync();
        }
    }
}
