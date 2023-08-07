using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
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
            await _context.Database.EnsureDeletedAsync();
            await _context.Database.EnsureCreatedAsync();
            await TrySeedAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while initialising the database.");
            throw;
        }
    }

    public async Task SeedAsync()
    {
        try
        {
            await TrySeedAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while seeding the database.");
            throw;
        }
    }

    public async Task TrySeedAsync()
    {
        // Default users
        var tester = new ApplicationUser { UserName = "tester@local.com", Email = "tester@local.com" };

        if (_userManager.Users.All(u => u.UserName != tester.UserName))
        {
            await _userManager.CreateAsync(tester, "Test!");
        }

        // Default data
        // Seed, if necessary
        /*
        if (!_context.Notifications.Any())
        {
            _context.TodoLists.Add(new TodoList
            {
                Title = "Todo List",
                Items =
                {
                    new TodoItem { Title = "Make a todo list 📃" },
                    new TodoItem { Title = "Check off the first item ✅" },
                    new TodoItem { Title = "Realise you've already done two things on the list! 🤯"},
                    new TodoItem { Title = "Reward yourself with a nice, long nap 🏆" },
                }
            });

            await _context.SaveChangesAsync();
        }
    */
    }
}
